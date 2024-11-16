
using Application.DTOs;
using Application.DTOs.Users;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;

public interface IUserService
{
    Task<PaginatedResult<UserDTO>> GetAllAsync(UserQueryDTO query);
    Task<UserDTO> GetByIdAsync(int id, string? includes = null);
    Task<UserDTO> CreateAsync(UserCreateDTO userCreateDTO);
    Task<UserDTO> UpdateAsync(int id, UserUpdateDTO userUpdateDTO);
    Task<UserDTO> PatchAsync(int id, UserPatchDTO userPatchDTO);
    Task DeleteAsync(int id);
    Task<UserDTO> UploadAvatarAsync(int id, IFormFile file);
    Task NotifyReceivedPackage(int userId);
    Task<UserDTO> GetMeAsync(int userId);
    Task UpdateCurrentPassword(int userId, UpdatePasswordDTO updatePasswordDTO);
}
