using AutoMapper;
using Domain.Entities;
using Application.Interfaces;
using Application.DTOs;
using Domain.Core.Repositories;
using Application.DTOs.BillDetails;
using Domain.Core.Specifications;

namespace Application.Implementations;

public class BillDetailService(IUnitOfWork unitOfWork, IMapper mapper) : IBillDetailService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<BillDetailDTO>> GetAllAsync(BillDetailQueryDTO query)
    {
        var spec = new BaseSpecification<BillDetail>(a => a.DeletedAt == null);
        var data = await _unitOfWork.Repository<BillDetail>().ListAsync(spec);
        var totalCount = await _unitOfWork.Repository<BillDetail>().CountAsync(spec);
        return new PaginatedResult<BillDetailDTO>(
            data.Select(_mapper.Map<BillDetailDTO>).ToList(),
            totalCount,
            query.Page,
            query.PageSize);
    }

    public async Task<BillDetailDTO> GetByIdAsync(int id, string? includes = null)
    {
        var billDetail = await _unitOfWork.Repository<BillDetail>().GetByIdAsync(id)
          //?? throw new EntityNotFoundException(nameof(BillDetail), id);
          ?? throw new Exception(nameof(BillDetail));
        return _mapper.Map<BillDetailDTO>(billDetail);
    }

    public async Task<BillDetailDTO> CreateAsync(BillDetailCreateDTO createDTO)
    {
        var billDetail = await _unitOfWork.Repository<BillDetail>().AddAsync(_mapper.Map<BillDetail>(createDTO));
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<BillDetailDTO>(billDetail);
    }

    public async Task<BillDetailDTO> UpdateAsync(int id, BillDetailUpdateDTO updateDTO)
    {
        var existingBillDetail = await _unitOfWork.Repository<BillDetail>().GetByIdAsync(id)
            //?? throw new EntityNotFoundException(nameof(BillDetail), id);
            ?? throw new Exception(nameof(BillDetail));
        _mapper.Map(updateDTO, existingBillDetail);
        _unitOfWork.Repository<BillDetail>().Update(existingBillDetail);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<BillDetailDTO>(existingBillDetail);
    }

    public async Task<BillDetailDTO> PatchAsync(int id, BillDetailPatchDTO patchDTO)
    {
        var existingBillDetail = await _unitOfWork.Repository<BillDetail>().GetByIdAsync(id)
            //?? throw new EntityNotFoundException(nameof(BillDetail), id);
            ?? throw new Exception(nameof(BillDetail));
        _mapper.Map(patchDTO, existingBillDetail);
        _unitOfWork.Repository<BillDetail>().Update(existingBillDetail);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<BillDetailDTO>(existingBillDetail);
    }

    public async Task DeleteAsync(int id)
    {
        var existingAnswer = await _unitOfWork.Repository<Answer>().GetByIdAsync(id)
                  //?? throw new EntityNotFoundException(nameof(Answer), id);
                  ?? throw new Exception(nameof(Answer));
        _unitOfWork.Repository<Answer>().Delete(existingAnswer);
        await _unitOfWork.SaveChangesAsync();
    }
}

