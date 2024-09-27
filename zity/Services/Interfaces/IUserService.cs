using zity.Utilities;
using zity.DTOs.Users;

namespace zity.Services.Interfaces
{
    public interface IUserService
    {
        Task<PaginatedResult<UserDTO>> GetAllAsync(UserQueryDTO query);
    }
}
