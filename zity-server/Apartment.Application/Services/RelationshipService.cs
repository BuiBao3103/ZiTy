using Apartment.Application.Core.Utilities;
using Apartment.Application.DTOs;
using Apartment.Application.DTOs.Relationships;
using Apartment.Application.Interfaces;
using AutoMapper;
using Apartment.Domain.Core.Repositories;
using Apartment.Domain.Core.Specifications;
using Apartment.Domain.Entities;
using Apartment.Domain.Exceptions;
using System.Net.Http;
using Newtonsoft.Json;
using Apartment.Application.DTOs.IdentityService;

namespace Apartment.Application.Services;

public class RelationshipService(IUnitOfWork unitOfWork, IMapper mapper, HttpClient httpClient) : IRelationshipService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HttpClient _httpClient = httpClient;

    public async Task<PaginatedResult<RelationshipDTO>> GetAllAsync(RelationshipQueryDTO query)
    {
        var filterExpression = query.BuildFilterCriteria<Relationship>(a => a.DeletedAt == null);
        var spec = new BaseSpecification<Relationship>(filterExpression);
        var totalCount = await _unitOfWork.Repository<Relationship>().CountAsync(spec);
        var includes = query.Includes?.Split(',').Select(include =>
            char.ToUpper(include[0]) + include.Substring(1)).ToList() ?? [];

        foreach (string include in includes)
        {
            if (include.StartsWith("User")) continue;
            spec.AddInclude(include);
        }
        if (!string.IsNullOrEmpty(query.Sort))
            if (query.Sort.StartsWith("-"))
                spec.ApplyOrderByDescending(query.Sort[1..]);
            else
                spec.ApplyOrderBy(query.Sort);
        spec.ApplyPaging(query.PageSize * (query.Page - 1), query.PageSize);
        var data = await _unitOfWork.Repository<Relationship>().ListAsync(spec);

        var paginatedData = new PaginatedResult<RelationshipDTO>(
            data.Select(_mapper.Map<RelationshipDTO>).ToList(),
            totalCount,
            query.Page,
            query.PageSize);
        if (includes.Contains("User"))
        {
            var relationshipTasks = paginatedData.Contents.Select(async (relationship, index) =>
            {
                var relationshipsResponse = await _httpClient.GetStringAsync($"http://localhost:8080/api/users/{relationship.UserId}");
                var user = JsonConvert.DeserializeObject<UserDTO>(relationshipsResponse);
                paginatedData.Contents[index].User = user;
            });

            await Task.WhenAll(relationshipTasks); // Đợi tất cả các tác vụ HTTP hoàn thành
        }
        return paginatedData;
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
        if(newRelationship.Role == "OWNER")
        {
            var relationshipSpec = new BaseSpecification<Relationship>(r=> r.Role == "OWNER" && r.ApartmentId == newRelationship.ApartmentId);
            var existingOwner = await _unitOfWork.Repository<Relationship>().FirstOrDefaultAsync(relationshipSpec);
            if (existingOwner != null)
            {
                throw new BusinessRuleException("There is already an owner in this apartment");
            }
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

