﻿using Application.Core.Utilities;
using Application.DTOs;
using Application.DTOs.Relationships;
using Application.Interfaces;
using AutoMapper;
using Domain.Core.Repositories;
using Domain.Core.Specifications;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services;

public class RelationshipService(IUnitOfWork unitOfWork, IMapper mapper) : IRelationshipService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<RelationshipDTO>> GetAllAsync(RelationshipQueryDTO query)
    {
        var filterExpression = query.BuildFilterCriteria<Relationship>(a => a.DeletedAt == null);
        var spec = new BaseSpecification<Relationship>(filterExpression);
        var totalCount = await _unitOfWork.Repository<Relationship>().CountAsync(spec);
        query.Includes?.Split(',').Select(i => char.ToUpper(i[0]) + i[1..]).ToList().ForEach(spec.AddInclude);
        if (!string.IsNullOrEmpty(query.Sort))
            if (query.Sort.StartsWith("-"))
                spec.ApplyOrderByDescending(query.Sort[1..]);
            else
                spec.ApplyOrderBy(query.Sort);
        spec.ApplyPaging(query.PageSize * (query.Page - 1), query.PageSize);
        var data = await _unitOfWork.Repository<Relationship>().ListAsync(spec);
        return new PaginatedResult<RelationshipDTO>(
            data.Select(_mapper.Map<RelationshipDTO>).ToList(),
            totalCount,
            query.Page,
            query.PageSize);
    }

    public async Task<RelationshipDTO> GetByIdAsync(int id, string? includes = null)
    {
        var spec = new BaseSpecification<Relationship>(a => a.DeletedAt == null && a.Id == id);
        includes?.Split(',').Select(i => char.ToUpper(i[0]) + i[1..]).ToList().ForEach(spec.AddInclude);
        var relationship = await _unitOfWork.Repository<Relationship>().FirstOrDefaultAsync(spec)
            ?? throw new EntityNotFoundException(nameof(Relationship), id);
        return _mapper.Map<RelationshipDTO>(relationship);
    }

    public async Task<RelationshipDTO> CreateAsync(RelationshipCreateDTO createDTO)
    {
        var newRelationship = _mapper.Map<Relationship>(createDTO);
        if (newRelationship.Role == "OWNER")
        {
            var existingOwner = await _unitOfWork.Repository<Relationship>().FirstOrDefaultAsync(
                new BaseSpecification<Relationship>(a => a.DeletedAt == null && a.ApartmentId == newRelationship.ApartmentId && a.Role == "OWNER"));
            if (existingOwner != null)
                throw new BusinessRuleException("There is already an owner in this apartment");
        }
        var relationship = await _unitOfWork.Repository<Relationship>().AddAsync(newRelationship);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<RelationshipDTO>(relationship);
    }

    public async Task<RelationshipDTO> UpdateAsync(int id, RelationshipUpdateDTO updateDTO)
    {
        var existingRelationship = await _unitOfWork.Repository<Relationship>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(Relationship), id);
        _mapper.Map(updateDTO, existingRelationship);
        _unitOfWork.Repository<Relationship>().Update(existingRelationship);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<RelationshipDTO>(existingRelationship);
    }

    public async Task<RelationshipDTO> PatchAsync(int id, RelationshipPatchDTO patchDTO)
    {
        var existingRelationship = await _unitOfWork.Repository<Relationship>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(Relationship), id);
        _mapper.Map(patchDTO, existingRelationship);
        _unitOfWork.Repository<Relationship>().Update(existingRelationship);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<RelationshipDTO>(existingRelationship);
    }

    public async Task DeleteAsync(int id)
    {
        var existingRelationship = await _unitOfWork.Repository<Relationship>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(Relationship), id);
        _unitOfWork.Repository<Relationship>().Delete(existingRelationship);
        await _unitOfWork.SaveChangesAsync();
    }
}

