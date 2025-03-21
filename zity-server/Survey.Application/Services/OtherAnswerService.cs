﻿using AutoMapper;
using Survey.Application.Core.Utilities;
using Survey.Application.DTOs;
using Survey.Application.DTOs.OtherAnswers;
using Survey.Application.Interfaces;
using Survey.Domain.Core.Repositories;
using Survey.Domain.Core.Specifications;
using Survey.Domain.Entities;
using Survey.Domain.Exceptions;

namespace Survey.Application.Services;

public class OtherAnswerService(IUnitOfWork unitOfWork, IMapper mapper) : IOtherAnswerService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<OtherAnswerDTO>> GetAllAsync(OtherAnswerQueryDTO query)
    {
        var filterExpression = query.BuildFilterCriteria<OtherAnswer>(a => a.DeletedAt == null);
        var spec = new BaseSpecification<OtherAnswer>(filterExpression);
        var totalCount = await _unitOfWork.Repository<OtherAnswer>().CountAsync(spec);
        query.Includes?.Split(',').Select(i => char.ToUpper(i[0]) + i[1..]).ToList().ForEach(spec.AddInclude);
        if (!string.IsNullOrEmpty(query.Sort))
            if (query.Sort.StartsWith("-"))
                spec.ApplyOrderByDescending(query.Sort[1..]);
            else
                spec.ApplyOrderBy(query.Sort);
        spec.ApplyPaging(query.PageSize * (query.Page - 1), query.PageSize);
        var data = await _unitOfWork.Repository<OtherAnswer>().ListAsync(spec);
        return new PaginatedResult<OtherAnswerDTO>(
            data.Select(_mapper.Map<OtherAnswerDTO>).ToList(),
            totalCount,
            query.Page,
            query.PageSize);
    }


    public async Task<OtherAnswerDTO> GetByIdAsync(int id, string? includes = null)
    {
        var spec = new BaseSpecification<OtherAnswer>(a => a.DeletedAt == null && a.Id == id);
        includes?.Split(',').Select(i => char.ToUpper(i[0]) + i[1..]).ToList().ForEach(spec.AddInclude);
        var otherAnswer = await _unitOfWork.Repository<OtherAnswer>().FirstOrDefaultAsync(spec)
            ?? throw new EntityNotFoundException(nameof(OtherAnswer), id);
        return _mapper.Map<OtherAnswerDTO>(otherAnswer);
    }
    public async Task<OtherAnswerDTO> CreateAsync(OtherAnswerCreateDTO createDTO)
    {
        var otherAnswer = await _unitOfWork.Repository<OtherAnswer>().AddAsync(_mapper.Map<OtherAnswer>(createDTO));
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<OtherAnswerDTO>(otherAnswer);
    }
    public async Task<OtherAnswerDTO> UpdateAsync(int id, OtherAnswerUpdateDTO updateDTO)
    {
        var existingOtherAnswer = await _unitOfWork.Repository<OtherAnswer>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(OtherAnswer), id);
        _mapper.Map(updateDTO, existingOtherAnswer);
        _unitOfWork.Repository<OtherAnswer>().Update(existingOtherAnswer);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<OtherAnswerDTO>(existingOtherAnswer);
    }

    public async Task<OtherAnswerDTO> PatchAsync(int id, OtherAnswerPatchDTO patchDTO)
    {
        var existingOtherAnswer = await _unitOfWork.Repository<OtherAnswer>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(OtherAnswer), id);
        _mapper.Map(patchDTO, existingOtherAnswer);
        _unitOfWork.Repository<OtherAnswer>().Update(existingOtherAnswer);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<OtherAnswerDTO>(existingOtherAnswer);
    }

    public async Task DeleteAsync(int id)
    {
        var existingOtherAnswer = await _unitOfWork.Repository<OtherAnswer>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(OtherAnswer), id);
        _unitOfWork.Repository<OtherAnswer>().Delete(existingOtherAnswer);
        await _unitOfWork.SaveChangesAsync();
    }
}
