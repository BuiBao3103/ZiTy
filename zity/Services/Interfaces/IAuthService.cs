using zity.DTOs.Auth;
using zity.Models;

namespace zity.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDto?> AuthenticateAsync(LoginDto loginDto);
        string GenerateJwtToken(User user, bool isRefreshToken);
        Task<TokenDto> RefreshTokenAsync(string refreshToken);
        Task RevokeRefreshTokenAsync(string refreshToken);
    }
}
