using AutoMapper;
using Billing.Domain.Entities;
using Billing.Domain.Core.Repositories;
using Billing.Domain.Core.Specifications;
using Billing.Domain.Exceptions;
using Billing.Application.Core.Utilities;
using Billing.Application.DTOs.BillDetails;
using Billing.Application.DTOs;
using Billing.Application.Interfaces;

namespace Billing.Application.Services;

public class BillDetailService(IUnitOfWork unitOfWork, IMapper mapper) : IBillDetailService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<BillDetailDTO>> GetAllAsync(BillDetailQueryDTO query)
    {
        var filterExpression = query.BuildFilterCriteria<BillDetail>(a => a.DeletedAt == null);
        var spec = new BaseSpecification<BillDetail>(filterExpression);
        var totalCount = await _unitOfWork.Repository<BillDetail>().CountAsync(spec);
        query.Includes?.Split(',').Select(i => char.ToUpper(i[0]) + i[1..]).ToList().ForEach(spec.AddInclude);
        if (!string.IsNullOrEmpty(query.Sort))
            if (query.Sort.StartsWith("-"))
                spec.ApplyOrderByDescending(query.Sort[1..]);
            else
                spec.ApplyOrderBy(query.Sort);
        spec.ApplyPaging(query.PageSize * (query.Page - 1), query.PageSize);
        var data = await _unitOfWork.Repository<BillDetail>().ListAsync(spec);
        return new PaginatedResult<BillDetailDTO>(
            data.Select(_mapper.Map<BillDetailDTO>).ToList(),
            totalCount,
            query.Page,
            query.PageSize);
    }

    public async Task<BillDetailDTO> GetByIdAsync(int id, string? includes = null)
    {
        var spec = new BaseSpecification<BillDetail>(a => a.DeletedAt == null && a.Id == id);
        includes?.Split(',').Select(i => char.ToUpper(i[0]) + i[1..]).ToList().ForEach(spec.AddInclude);
        var billDetail = await _unitOfWork.Repository<BillDetail>().FirstOrDefaultAsync(spec)
            ?? throw new EntityNotFoundException(nameof(BillDetail), id);
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
            ?? throw new EntityNotFoundException(nameof(BillDetail), id);
        _mapper.Map(updateDTO, existingBillDetail);
        _unitOfWork.Repository<BillDetail>().Update(existingBillDetail);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<BillDetailDTO>(existingBillDetail);
    }

    public async Task<BillDetailDTO> PatchAsync(int id, BillDetailPatchDTO patchDTO)
    {
        var existingBillDetail = await _unitOfWork.Repository<BillDetail>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(BillDetail), id);
        _mapper.Map(patchDTO, existingBillDetail);
        _unitOfWork.Repository<BillDetail>().Update(existingBillDetail);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<BillDetailDTO>(existingBillDetail);
    }

    public async Task DeleteAsync(int id)
    {
        var existingBillDetail = await _unitOfWork.Repository<BillDetail>().GetByIdAsync(id)
                  ?? throw new EntityNotFoundException(nameof(BillDetail), id);
        _unitOfWork.Repository<BillDetail>().Delete(existingBillDetail);
        await _unitOfWork.SaveChangesAsync();
    }
}

