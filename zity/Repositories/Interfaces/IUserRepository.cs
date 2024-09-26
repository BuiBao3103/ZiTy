using zity.DTOs.Users;
using zity.Utilities;
using ZiTy.Models;

namespace ZiTy.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<PaginatedResult<User>> GetAllAsync(UserQueryDto query);
    }
}
