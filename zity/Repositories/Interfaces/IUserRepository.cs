using zity.Utilities;
using zity.Models;
using zity.DTOs.Users;

namespace zity.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<PaginatedResult<User>> GetAllAsync(UserQueryDTO query);
        Task<User?> GetByIdAsync(int id, string? includes);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<User> GetByUsernameAsync(string username);
    }
}