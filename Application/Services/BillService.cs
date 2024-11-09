using AutoMapper;
using Application.DTOs.Bills;
using Domain.Entities;
using Application.Interfaces;
using Domain.Core.Repositories;
using Domain.Core.Specifications;
using Application.DTOs;

using Application.DTOs.Momo;
using Application.Core.Services;
using Domain.Exceptions;

namespace Application.Services;

public class BillService(IUnitOfWork unitOfWork, IMapper mapper, IVNPayService vnpayService, IMomoService momoService) : IBillService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    private readonly IVNPayService _vnpayService = vnpayService;
    private readonly IMomoService _momoService = momoService;

    public async Task<PaginatedResult<BillDTO>> GetAllAsync(BillQueryDTO query)
    {
        var spec = new BaseSpecification<Bill>(a => a.DeletedAt == null);
        var totalCount = await _unitOfWork.Repository<Bill>().CountAsync(spec);
        query.Includes?.Split(',').ToList().ForEach(spec.AddInclude);
        if (!string.IsNullOrEmpty(query.Sort))
            if (query.Sort.StartsWith("-"))
                spec.ApplyOrderByDescending(query.Sort[1..]);
            else
                spec.ApplyOrderBy(query.Sort);
        spec.ApplyPaging(query.PageSize * (query.Page - 1), query.PageSize);
        var data = await _unitOfWork.Repository<Bill>().ListAsync(spec);
        return new PaginatedResult<BillDTO>(
            data.Select(_mapper.Map<BillDTO>).ToList(),
            totalCount,
            query.Page,
            query.PageSize);
    }

    public async Task<BillDTO> GetByIdAsync(int id, string? includes = null)
    {
        var bill = await _unitOfWork.Repository<Bill>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(Bill), id);
        return _mapper.Map<BillDTO>(bill);
    }
    public async Task<BillDTO> CreateAsync(BillCreateDTO createDTO)
    {
        var bill = await _unitOfWork.Repository<Bill>().AddAsync(_mapper.Map<Bill>(createDTO));
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<BillDTO>(bill);
    }

    public async Task<BillDTO> UpdateAsync(int id, BillUpdateDTO updateDTO)
    {
        var existingBill = await _unitOfWork.Repository<Bill>().GetByIdAsync(id)
             ?? throw new EntityNotFoundException(nameof(Bill), id);
        _mapper.Map(updateDTO, existingBill);
        _unitOfWork.Repository<Bill>().Update(existingBill);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<BillDTO>(existingBill);
    }

    public async Task<BillDTO> PatchAsync(int id, BillPatchDTO patchDTO)
    {
        var existingBill = await _unitOfWork.Repository<Bill>().GetByIdAsync(id)
                ?? throw new EntityNotFoundException(nameof(Bill), id);
        _mapper.Map(patchDTO, existingBill);
        _unitOfWork.Repository<Bill>().Update(existingBill);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<BillDTO>(existingBill);
    }

    public async Task DeleteAsync(int id)
    {
        var existingBill = await _unitOfWork.Repository<Bill>().GetByIdAsync(id)
                ?? throw new EntityNotFoundException(nameof(Bill), id);
        _unitOfWork.Repository<Bill>().Delete(existingBill);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<string> CreatePaymentVNPayAsync(int id)
    {
        var existingBill = await _unitOfWork.Repository<Bill>().GetByIdAsync(id)
                ?? throw new EntityNotFoundException(nameof(Bill), id);
        var paymentUrl = _vnpayService.CreatePaymentUrl(existingBill);
        return paymentUrl;
    }

    public async Task<MomoCreatePaymentDto> CreatePaymentMomoAsync(int id, MomoRequestCreatePaymentDto request)
    {
        var existingBill = await _unitOfWork.Repository<Bill>().GetByIdAsync(id)
                ?? throw new EntityNotFoundException(nameof(Bill), id);
        var momoCreatePaymentDto = await _momoService.CreatePaymentAsync(existingBill, request);
        return momoCreatePaymentDto;
    }

    public async Task HandleMoMoCallBackAsync(int id, MomoCallBackDto callbackDto)
    {
        if (callbackDto.ResultCode == 0)
        {
            var bill = await _unitOfWork.Repository<Bill>().GetByIdAsync(id);
            if (bill == null)
            {
                return;
            }
            bill.Status = "Paid";
            _unitOfWork.Repository<Bill>().Update(bill);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

