using AutoMapper;
using Application.DTOs.Bills;
using Domain.Entities;
using Application.Interfaces;
using Domain.Core.Repositories;
using Domain.Core.Specifications;
using Application.DTOs;

using Application.DTOs.Momo;
using Application.Core.Services;

namespace Application.Services;

public class BillService(IUnitOfWork unitOfWork, IMapper mapper) : IBillService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<BillDTO>> GetAllAsync(BillQueryDTO queryParam)
    {
        var spec = new BaseSpecification<Bill>(b => b.DeletedAt == null);
        var data = await _unitOfWork.Repository<Bill>().ListAsync(spec);
        var totalCount = await _unitOfWork.Repository<Bill>().CountAsync(spec);

        var bills = data.Select(_mapper.Map<BillDTO>).ToList();
        return new PaginatedResult<BillDTO>(
            bills,
            totalCount,
            queryParam.Page,
            queryParam.PageSize);
    }

    public async Task<BillDTO> GetByIdAsync(int id, string? includes = null)
    {
        var bill = await _unitOfWork.Repository<Bill>().GetByIdAsync(id)
            //?? throw new EntityNotFoundException(nameof(Bill), id);
            ?? throw new Exception(nameof(Bill));
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
                //?? throw new EntityNotFoundException(nameof(Bill), id);
                ?? throw new Exception(nameof(Bill));
        _mapper.Map(updateDTO, existingBill);
        _unitOfWork.Repository<Bill>().Update(existingBill);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<BillDTO>(existingBill);
    }

    public async Task<BillDTO> PatchAsync(int id, BillPatchDTO patchDTO)
    {
        var existingBill = await _unitOfWork.Repository<Bill>().GetByIdAsync(id)
                //?? throw new EntityNotFoundException(nameof(Bill), id);
                ?? throw new Exception(nameof(Bill));
        _mapper.Map(patchDTO, existingBill);
        _unitOfWork.Repository<Bill>().Update(existingBill);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<BillDTO>(existingBill);
    }

    public async Task DeleteAsync(int id)
    {
        var existingBill = await _unitOfWork.Repository<Bill>().GetByIdAsync(id)
                //?? throw new EntityNotFoundException(nameof(Bill), id);
                ?? throw new Exception(nameof(Bill));
        _unitOfWork.Repository<Bill>().Delete(existingBill);
        await _unitOfWork.SaveChangesAsync();
    }

    // public async Task<string> CreatePaymentVNPayAsync(int id)
    // {
    //     var existingBill = await _unitOfWork.Repository<Bill>().GetByIdAsync(id)
    //                             // ?? throw new EntityNotFoundException(nameof(Bill), id);
    //                             ?? throw new Exception(nameof(Bill));
    //     var paymentUrl = IVNPayService.CreatePaymentUrl(existingBill);
    //     return paymentUrl;
    // }

    // public async Task<MomoCreatePaymentDto> CreatePaymentMomoAsync(int id, MomoRequestCreatePaymentDto request)
    // {
    //     var existingBill = await _unitOfWork.Repository<Bill>().GetByIdAsync(id)
    //     // ?? throw new EntityNotFoundException(nameof(Bill), id);
    //                                     ?? throw new Exception(nameof(Bill));
    //     var momoCreatePaymentDto = await IMomoService.CreatePaymentAsync(existingBill, request);
    //     return momoCreatePaymentDto;
    // }

    public async Task HandleMoMoCallBackAsync(int id, MomoCallBackDto callbackDto)
    {
        if (callbackDto.ResultCode == 0)
        {
            var bill = await _unitOfWork.Repository<Bill>().GetByIdAsync(id)
                ?? throw new Exception(nameof(Bill));
            bill.Status = "Paid";
            _unitOfWork.Repository<Bill>().Update(bill);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

