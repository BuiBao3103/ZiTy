using zity.DTOs.Relationships;
using zity.DTOs.Users;
using zity.Utilities;

namespace zity.Services.Interfaces
{
    public interface IRelationshipService
    {
        Task<PaginatedResult<RelationshipDTO>> GetAllAsync(RelationshipQueryDTO query);
    }
}
