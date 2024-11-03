using Application.DTOs;
using Application.DTOs.Relationships;
using Application.Interfaces;
using AutoMapper;
using Domain.Core.Repositories;
using Domain.Core.Specifications;
using Domain.Entities;

namespace Application.Services;

public class RelationshipService(IUnitOfWork unitOfWork, IMapper mapper) : IRelationshipService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<RelationshipDTO>> GetAllAsync(RelationshipQueryDTO query)
    {
        var spec = new BaseSpecification<Relationship>(a => a.DeletedAt == null);
        var data = await _unitOfWork.Repository<Relationship>().ListAsync(spec);
        var totalCount = await _unitOfWork.Repository<Relationship>().CountAsync(spec);
        return new PaginatedResult<RelationshipDTO>(
            data.Select(_mapper.Map<RelationshipDTO>).ToList(),
            totalCount,
            query.Page,
            query.PageSize);
    }

    public async Task<RelationshipDTO> GetByIdAsync(int id, string? includes = null)
    {
        var relationship = await _unitOfWork.Repository<Relationship>().GetByIdAsync(id)
           //?? throw new EntityNotFoundException(nameof(Relationship), id);
           ?? throw new Exception(nameof(Relationship));
        return _mapper.Map<RelationshipDTO>(relationship);
    }

    public async Task<RelationshipDTO> CreateAsync(RelationshipCreateDTO createDTO)
    {
        var relationship = await _unitOfWork.Repository<Relationship>().AddAsync(_mapper.Map<Relationship>(createDTO));
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<RelationshipDTO>(relationship);
    }

    public async Task<RelationshipDTO> UpdateAsync(int id, RelationshipUpdateDTO updateDTO)
    {
        var existingRelationship = await _unitOfWork.Repository<Relationship>().GetByIdAsync(id)
          //?? throw new EntityNotFoundException(nameof(Relationship), id);
          ?? throw new Exception(nameof(Relationship));
        _mapper.Map(updateDTO, existingRelationship);
        _unitOfWork.Repository<Relationship>().Update(existingRelationship);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<RelationshipDTO>(existingRelationship);
    }

    public async Task<RelationshipDTO> PatchAsync(int id, RelationshipPatchDTO patchDTO)
    {
        var existingRelationship = await _unitOfWork.Repository<Relationship>().GetByIdAsync(id)
            //?? throw new EntityNotFoundException(nameof(Relationship), id);
            ?? throw new Exception(nameof(Relationship));
        _mapper.Map(patchDTO, existingRelationship);
        _unitOfWork.Repository<Relationship>().Update(existingRelationship);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<RelationshipDTO>(existingRelationship);
    }

    public async Task DeleteAsync(int id)
    {
        var existingRelationship = await _unitOfWork.Repository<Relationship>().GetByIdAsync(id)
                   //?? throw new EntityNotFoundException(nameof(Relationship), id);
                   ?? throw new Exception(nameof(Relationship));
        _unitOfWork.Repository<Relationship>().Delete(existingRelationship);
        await _unitOfWork.SaveChangesAsync();
    }
}

