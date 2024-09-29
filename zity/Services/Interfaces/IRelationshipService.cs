using zity.DTOs.Relationships;
using zity.Utilities;

namespace zity.Services.Interfaces
{
    public interface IRelationshipService
    {
        Task<PaginatedResult<RelationshipDto>> GetAllAsync(RelationshipQueryDTO query);
        Task<RelationshipDto> GetByIdAsync(int id, string includes);

    }
}
