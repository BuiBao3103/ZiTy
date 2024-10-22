using System.Security.Claims;
using zity.DTOs.Auth;
using zity.Models;

namespace zity.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDto?> AuthenticateAsync(LoginDto loginDto);
        string GenerateJwtToken(User user);
    }
}
