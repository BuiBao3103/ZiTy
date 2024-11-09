
using Application.DTOs;
using Application.DTOs.Users;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;

public interface IUserService
{
    Task<PaginatedResult<UserDTO>> GetAllAsync(UserQueryDTO query);
    Task<UserDTO> GetByIdAsync(int id, string? includes = null);
    Task<UserDTO> CreateAsync(UserCreateDTO userCreateDTO);
    Task<UserDTO> UploadAvatarAsync(int id, IFormFile file);
    Task NotifyReceivedPackage(int userId);
    Task<UserDTO> GetMeAsync(int userId);
}
