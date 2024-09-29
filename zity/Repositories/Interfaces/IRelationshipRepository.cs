using zity.DTOs.Relationships;
using zity.DTOs.Users;
using zity.Models;
using zity.Utilities;

namespace zity.Repositories.Interfaces
{
    public interface IRelationshipRepository
    {
        Task<PaginatedResult<Relationship>> GetAllAsync(RelationshipQueryDTO query);
        Task<Relationship> GetByIdAsync(int id, string includes);
    }
}
