using zity.DTOs.Relationships;
using zity.Models;
using zity.Utilities;

namespace zity.Services.Interfaces
{
    public interface IRelationshipService
    {
        Task<PaginatedResult<RelationshipDTO>> GetAllAsync(RelationshipQueryDTO query);
        Task<RelationshipDTO?> GetByIdAsync(int id, string? includes);
        Task<RelationshipDTO> CreateAsync(RelationshipCreateDTO relationshipCreateDTO);
        //Task<RelationshipDTO?> UpdateAsync(int id, RelationshipUpdateDTO relationshipUpdateDTO);
        //Task<RelationshipDTO?> PatchAsync(int id, RelationshipPatchDTO relationshipPatchDTO);
        Task<bool> DeleteAsync(int id);
    }
}
