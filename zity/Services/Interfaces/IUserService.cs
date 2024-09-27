using ZiTy.Utilities;
using ZiTy.DTOs.Users;

namespace ZiTy.Services.Interfaces
{
    public interface IUserService
    {
        Task<PaginatedResult<UserDto>> GetAllAsync(UserQueryDto query);
    }
}
