using System.Collections.Generic;
using System.Threading.Tasks;
using zity.DTOs.Users;
using zity.Utilities;
using ZiTy.DTOs.Users;

namespace ZiTy.Services.Interfaces
{
    public interface IUserService
    {
        Task<PaginatedResult<UserDto>> GetAllAsync(UserQueryDto query);
    }
}
