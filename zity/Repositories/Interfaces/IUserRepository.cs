using zity.Utilities;
using zity.Models;
using zity.DTOs.Users;

namespace zity.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<PaginatedResult<User>> GetAllAsync(UserQueryDTO query);
    }
}