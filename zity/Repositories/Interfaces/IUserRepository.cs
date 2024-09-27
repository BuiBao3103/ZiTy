using ZiTy.Utilities;
using ZiTy.Models;
using ZiTy.DTOs.Users;

namespace ZiTy.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<PaginatedResult<User>> GetAllAsync(UserQueryDto query);
    }
}
