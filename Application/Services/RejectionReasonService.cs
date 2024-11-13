using Application.Core.Utilities;
using Application.DTOs;
using Application.DTOs.RejectionReasons;
using Application.Interfaces;
using AutoMapper;
using Domain.Core.Repositories;
using Domain.Core.Specifications;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services;

public class RejectionReasonService(IUnitOfWork unitOfWork, IMapper mapper) : IRejectionReasonService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<PaginatedResult<RejectionReasonDTO>> GetAllAsync(RejectionReasonQueryDTO query)
    {
        var filterExpression = query.BuildFilterCriteria<RejectionReason>(a => a.DeletedAt == null);
        var spec = new BaseSpecification<RejectionReason>(filterExpression);
        var totalCount = await _unitOfWork.Repository<RejectionReason>().CountAsync(spec);
        query.Includes?.Split(',').Select(i => char.ToUpper(i[0]) + i[1..]).ToList().ForEach(spec.AddInclude);
        if (!string.IsNullOrEmpty(query.Sort))
            if (query.Sort.StartsWith("-"))
                spec.ApplyOrderByDescending(query.Sort[1..]);
            else
                spec.ApplyOrderBy(query.Sort);
        spec.ApplyPaging(query.PageSize * (query.Page - 1), query.PageSize);
        var data = await _unitOfWork.Repository<RejectionReason>().ListAsync(spec);
        return new PaginatedResult<RejectionReasonDTO>(
            data.Select(_mapper.Map<RejectionReasonDTO>).ToList(),
            totalCount,
            query.Page,
            query.PageSize);
    }


    public async Task<RejectionReasonDTO> GetByIdAsync(int id, string? includes = null)
    {
        var spec = new BaseSpecification<RejectionReason>(a => a.DeletedAt == null && a.Id == id);
        includes?.Split(',').Select(i => char.ToUpper(i[0]) + i[1..]).ToList().ForEach(spec.AddInclude);
        var rejectionReason = await _unitOfWork.Repository<RejectionReason>().FirstOrDefaultAsync(spec)
            ?? throw new EntityNotFoundException(nameof(RejectionReason), id);
        return _mapper.Map<RejectionReasonDTO>(rejectionReason);
    }
    public async Task<RejectionReasonDTO> CreateAsync(RejectionReasonCreateDTO createDTO)
    {
        var rejectionReason = await _unitOfWork.Repository<RejectionReason>().AddAsync(_mapper.Map<RejectionReason>(createDTO));
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<RejectionReasonDTO>(rejectionReason);
    }
    public async Task<RejectionReasonDTO> UpdateAsync(int id, RejectionReasonUpdateDTO updateDTO)
    {
        var existingRejectionReason = await _unitOfWork.Repository<RejectionReason>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(RejectionReason), id);
        _mapper.Map(updateDTO, existingRejectionReason);
        _unitOfWork.Repository<RejectionReason>().Update(existingRejectionReason);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<RejectionReasonDTO>(existingRejectionReason);
    }

    public async Task<RejectionReasonDTO> PatchAsync(int id, RejectionReasonPatchDTO patchDTO)
    {
        var existingRejectionReason = await _unitOfWork.Repository<RejectionReason>().GetByIdAsync(id)
             ?? throw new EntityNotFoundException(nameof(RejectionReason), id);
        _mapper.Map(patchDTO, existingRejectionReason);
        _unitOfWork.Repository<RejectionReason>().Update(existingRejectionReason);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<RejectionReasonDTO>(existingRejectionReason);
    }

    public async Task DeleteAsync(int id)
    {
        var existingRejectionReason = await _unitOfWork.Repository<RejectionReason>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(RejectionReason), id);
        _unitOfWork.Repository<RejectionReason>().Delete(existingRejectionReason);
        await _unitOfWork.SaveChangesAsync();
    }

}

