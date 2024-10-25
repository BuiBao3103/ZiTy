using zity.Utilities;
using zity.DTOs.Users;

namespace zity.Services.Interfaces
{
    public interface IUserService
    {
        Task<PaginatedResult<UserDTO>> GetAllAsync(UserQueryDTO query);
        Task<UserDTO> GetByIdAsync(int id, string? includes);
        Task<UserDTO> CreateAsync(UserCreateDTO userCreateDTO);
        Task<UserDTO> UploadAvatarAsync(int id, IFormFile file);
        Task NotifyReceivedPackage(int userId);
        Task<UserDTO> GetMeAsync(int userId, string? includes = null);
    }
}
