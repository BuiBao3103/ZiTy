using System.Security.Claims;
using zity.DTOs.Auth;
using zity.Models;

namespace zity.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDto> AuthenticateAsync(LoginDto loginDto);
        //Task<TokenDto> RefreshTokenAsync(string token, string refreshToken);
        //Task<bool> RevokeTokenAsync(string username);
        string GenerateJwtToken(User user);
        //RefreshToken GenerateRefreshToken();
        //ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
