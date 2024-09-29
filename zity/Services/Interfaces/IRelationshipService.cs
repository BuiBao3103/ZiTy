using zity.DTOs.Relationships;
using zity.Utilities;

namespace zity.Services.Interfaces
{
    public interface IRelationshipService
    {
        Task<PaginatedResult<RelationshipDTO>> GetAllAsync(RelationshipQueryDTO query);
        Task<RelationshipDTO> GetByIdAsync(int id, string includes);

    }
}
