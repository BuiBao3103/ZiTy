using AutoMapper;
using Billing.Application.Core.Constants;
using Billing.Application.DTOs.Settings;
using Billing.Application.Interfaces;
using Billing.Domain.Core.Repositories;
using Billing.Domain.Entities;
using Billing.Domain.Enums;
using Billing.Domain.Exceptions;
using Billing.Domain.Core.Specifications;
using System.Net.Http;
using Newtonsoft.Json;
using Billing.Application.DTOs.ApartmentService;
using System.Text;

namespace Billing.Application.Services;
public class SettingService(IUnitOfWork unitOfWork, IMapper mapper, HttpClient httpClient) : ISettingService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly HttpClient _httpClient = httpClient;

    public async Task<SettingDTO> GetSetting()
    {
        var setting = await _unitOfWork.Repository<Setting>().GetByIdAsync(SettingConstants.SettingId)
          ?? throw new EntityNotFoundException(nameof(Setting), SettingConstants.SettingId);
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

        var billSpec = new BaseSpecification<Bill>(b => b.DeletedAt == null && b.Status == "OVERDUE" && b.Monthly == setting.CurrentMonthly);
        var bills = await _unitOfWork.Repository<Bill>().ListAsync(billSpec);
        List<Task> updateApartmentTasks = new List<Task>();

        foreach (var bill in bills)
        {
            updateApartmentTasks.Add(Task.Run(async () =>
            {
                try
                {
                    // Fetch relationship details
                    var relationshipsResponse = await _httpClient.GetStringAsync($"http://localhost:8080/api/relationships/{bill.RelationshipId}");
                    var relationship = JsonConvert.DeserializeObject<RelationshipDTO>(relationshipsResponse);

                    // Prepare and send update
                    var content = new StringContent(
                        JsonConvert.SerializeObject(new { Status = "DISRUPTION" }),
                        Encoding.UTF8,
                        "application/json");

                    var response = await _httpClient.PatchAsync($"http://localhost:8080/api/apartments/{relationship.ApartmentId}", content);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    // Use Console.WriteLine or replace with appropriate logging
                    Console.WriteLine($"Failed to update apartment water index for relationship {bill.RelationshipId}: {ex.Message}");
                    throw;
                }
            }));
        }


        setting.SystemStatus = SystemStatusEnum.DELINQUENT;

        _unitOfWork.Repository<Setting>().Update(setting);
        await _unitOfWork.SaveChangesAsync();
        await Task.WhenAll(updateApartmentTasks);
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

        //var relationshipSpec = new BaseSpecification<Relationship>(r => r.DeletedAt == null && r.Role == "OWNER" && r.User.IsStaying == true);
        //relationshipSpec.AddInclude(r => r.Apartment);
        //var relationships = await _unitOfWork.Repository<Relationship>().ListAsync(relationshipSpec);

        var serviceSpec = new BaseSpecification<Service>(s => s.DeletedAt == null);
        var services = await _unitOfWork.Repository<Service>().ListAsync(serviceSpec);



        //foreach (var relationship in relationships)
        //{
        //    var billSpec = new BaseSpecification<Bill>(b => b.RelationshipId == relationship.Id && b.Monthly == setting.CurrentMonthly);
        //    var currentMonthlyBill = await _unitOfWork.Repository<Bill>().FirstOrDefaultAsync(billSpec);
        //    Console.WriteLine(currentMonthlyBill == null);
        //    if (currentMonthlyBill == null)
        //    {

        //        //init list billdetails and cal total price
        //        var billDetails = new List<BillDetail>();
        //        var totalServicePrice = 0.0f;
        //        foreach (var service in services)
        //        {
        //            var billDetail = new BillDetail()
        //            {
        //                ServiceId = service.Id,
        //                Price = service.Price,
        //            };
        //            billDetails.Add(billDetail);
        //            totalServicePrice += service.Price;
        //        }
        //        var totalRoomPrice = (setting.RoomPricePerM2 * relationship.Apartment.Area) * (100 + setting.RoomVat) / 100;
        //        var newBill = new Bill()
        //        {
        //            RelationshipId = relationship.Id,
        //            OldWater = relationship.Apartment.CurrentWaterNumber,
        //            Monthly = setting.CurrentMonthly,
        //            CreatedAt = DateTime.Now,
        //            Status = "UNPAID",
        //            TotalPrice = totalServicePrice + totalRoomPrice,
        //            BillDetails = billDetails
        //        };
        //        await _unitOfWork.Repository<Bill>().AddAsync(newBill);
        //    }
        //}


        _unitOfWork.Repository<Setting>().Update(setting);

        setting.SystemStatus = SystemStatusEnum.PREPAYMENT;

        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<SettingDTO>(setting);
    }
}
