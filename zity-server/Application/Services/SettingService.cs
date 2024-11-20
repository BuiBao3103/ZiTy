using Application.Core.Constants;
using Application.DTOs.Settings;
using Application.Interfaces;
using AutoMapper;
using Domain.Core.Repositories;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Core.Specifications;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;

namespace Application.Services;
public class SettingService(IUnitOfWork unitOfWork, IMapper mapper) : ISettingService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    public async Task<SettingDTO> GetSetting()
    {
        var setting = await _unitOfWork.Repository<Setting>().GetByIdAsync(SettingConstants.SettingId)
          ?? throw new EntityNotFoundException(nameof(Answer), SettingConstants.SettingId);
        return _mapper.Map<SettingDTO>(setting);
    }
    public async Task<SettingDTO> PatchSetting(SettingPatchDTO patchDTO)
    {

        var existingSetting = await _unitOfWork.Repository<Setting>().GetByIdAsync(SettingConstants.SettingId)
            ?? throw new EntityNotFoundException(nameof(Service), SettingConstants.SettingId);
        _mapper.Map(patchDTO, existingSetting);
        _unitOfWork.Repository<Setting>().Update(existingSetting);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<SettingDTO>(existingSetting);
    }

    public async Task<SettingDTO> TransitionToDelinquent()
    {
        var setting = await _unitOfWork.Repository<Setting>().GetByIdAsync(SettingConstants.SettingId)
            ?? throw new EntityNotFoundException(nameof(Service), SettingConstants.SettingId);

        var relationshipSpec = new BaseSpecification<Relationship>(r => r.DeletedAt == null && r.Role == "OWNER" && r.Bills.Any(b => b.Status == "OVERDUE"));
        relationshipSpec.AddInclude(r => r.Apartment);
        var relationships = await _unitOfWork.Repository<Relationship>().ListAsync(relationshipSpec);

        var distinctRelationships = relationships
            .GroupBy(r => r.Apartment.Id)
            .Select(g => g.First())
            .ToList();

        foreach (var relationship in distinctRelationships)
        {
            relationship.Apartment.Status = "DISRUPTION";
            _unitOfWork.Repository<Apartment>().Update(relationship.Apartment);
        }

        setting.SystemStatus = SystemStatusEnum.DELINQUENT;

        _unitOfWork.Repository<Setting>().Update(setting);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<SettingDTO>(setting);
    }

    public async Task<SettingDTO> TransitionToOverdue()
    {
        var setting = await _unitOfWork.Repository<Setting>().GetByIdAsync(SettingConstants.SettingId)
             ?? throw new EntityNotFoundException(nameof(Service), SettingConstants.SettingId);

        var billSpec = new BaseSpecification<Bill>(b => b.DeletedAt == null && b.Monthly == setting.CurrentMonthly && b.Status == "UNPAID");
        var bills = await _unitOfWork.Repository<Bill>().ListAsync(billSpec);

        foreach (var bill in bills)
        {
            bill.Status = "OVERDUE";
            _unitOfWork.Repository<Bill>().Update(bill);
        }
        setting.SystemStatus = SystemStatusEnum.OVERDUE;


        _unitOfWork.Repository<Setting>().Update(setting);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<SettingDTO>(setting);
    }

    public async Task<SettingDTO> TransitionToPayment()
    {
        var setting = await _unitOfWork.Repository<Setting>().GetByIdAsync(SettingConstants.SettingId)
            ?? throw new EntityNotFoundException(nameof(Service), SettingConstants.SettingId);

        var billSpec = new BaseSpecification<Bill>(b => b.DeletedAt == null && b.Monthly == setting.CurrentMonthly && b.NewWater == null);
        var bills = await _unitOfWork.Repository<Bill>().ListAsync(billSpec);

        if (bills.Count != 0)
        {
            throw new BusinessRuleException("There are still bills that have not been calculated water price");
        }

        setting.SystemStatus = SystemStatusEnum.PAYMENT;

        _unitOfWork.Repository<Setting>().Update(setting);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<SettingDTO>(setting);
    }

    public async Task<SettingDTO> TransitionToPrepayment()
    {
        var setting = await _unitOfWork.Repository<Setting>().GetByIdAsync(SettingConstants.SettingId)
           ?? throw new EntityNotFoundException(nameof(Service), SettingConstants.SettingId);

        var relationshipSpec = new BaseSpecification<Relationship>(r => r.DeletedAt == null && r.Role == "OWNER" && r.User.IsStaying == true);
        relationshipSpec.AddInclude(r => r.Apartment);
        var relationships = await _unitOfWork.Repository<Relationship>().ListAsync(relationshipSpec);

        var serviceSpec = new BaseSpecification<Service>(s => s.DeletedAt == null);
        var services = await _unitOfWork.Repository<Service>().ListAsync(serviceSpec);


        foreach (var relationship in relationships)
        {
            var billSpec = new BaseSpecification<Bill>(b => b.RelationshipId == relationship.Id && b.Status == "UNPAID" && b.Monthly == setting.CurrentMonthly);
            var currentMonthlyBill = await _unitOfWork.Repository<Bill>().FirstOrDefaultAsync(billSpec);
            if (currentMonthlyBill == null)
            {
                List<BillDetail> billDetails = new List<BillDetail>();
                var totalServicePrice = 0.0f;
                foreach (var service in services)
                {
                    BillDetail billDetail = new BillDetail()
                    {
                        ServiceId = service.Id,
                        Price = service.Price,
                    };
                    billDetails.Add(billDetail);
                    totalServicePrice += service.Price;
                }

                var totalRoomPrice = (setting.RoomPricePerM2 * relationship.Apartment.Area) * (100 + setting.RoomVat) / 100;


                Bill newBill = new Bill
                {
                    RelationshipId = relationship.Id,
                    Monthly = setting.CurrentMonthly,
                    CreatedAt = DateTime.Now,
                    Status = "UNPAID",
                    TotalPrice = totalServicePrice + totalRoomPrice,
                    BillDetails = billDetails
                };
                await _unitOfWork.Repository<Bill>().AddAsync(newBill);
            }
        }


        _unitOfWork.Repository<Setting>().Update(setting);

        setting.SystemStatus = SystemStatusEnum.PREPAYMENT;

        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<SettingDTO>(setting);
    }
}
