using Application.DTOs;
using Application.DTOs.Relationships;

namespace Application.Interfaces;

public interface IRelationshipService
{
    Task<PaginatedResult<RelationshipDTO>> GetAllAsync(RelationshipQueryDTO query);
    Task<RelationshipDTO> GetByIdAsync(int id, string? includes = null);
    Task<RelationshipDTO> CreateAsync(RelationshipCreateDTO relationshipCreateDTO);
    Task<RelationshipDTO> UpdateAsync(int id, RelationshipUpdateDTO relationshipUpdateDTO);
    Task<RelationshipDTO> PatchAsync(int id, RelationshipPatchDTO relationshipPatchDTO);
    Task DeleteAsync(int id);
}
