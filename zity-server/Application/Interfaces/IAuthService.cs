using Application.DTOs.Auth;
using Domain.Entities;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<TokenDto?> AuthenticateAsync(LoginDto loginDto);
    string GenerateJwtToken(User user, bool isRefreshToken);
    Task<TokenDto> RefreshTokenAsync(string refreshToken);
    Task UpdatePasswordFirstLoginAsync(int userId, UpdatePasswordFirstLoginDTO updatePasswordFirstLogin);
}
