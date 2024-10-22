using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using zity.Configuration;
using zity.DTOs.Auth;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;

namespace zity.Services.Implementations
{
    public class AuthService(IUserRepository userRepository, JWTSettings jwtSettings) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly JWTSettings _jwtSettings = jwtSettings;

        public async Task<TokenDto?> AuthenticateAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByUsernameAsync(loginDto.Username);
            if (user == null || !VerifyPassword(user, loginDto.Password))
                return null;

            var accessToken = GenerateJwtToken(user, false);
            var refreshToken = GenerateJwtToken(user, true);

            return new TokenDto
            {
                Token = accessToken,
                RefreshToken = refreshToken
            };
        }

        public string GenerateJwtToken(User user, bool isRefreshToken)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Role, user.UserType),
                new("tokenType", isRefreshToken ? "refresh" : "access")
            };

            var secretKey = isRefreshToken ? _jwtSettings.RefreshTokenKey : _jwtSettings.AccessTokenKey;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: isRefreshToken
                    ? DateTime.UtcNow.AddDays(_jwtSettings.RefreshExpirationInDays)
                    : DateTime.UtcNow.AddMinutes(_jwtSettings.AccessExpirationInMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<TokenDto> RefreshTokenAsync(string refreshToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var refreshKey = Encoding.UTF8.GetBytes(_jwtSettings.RefreshTokenKey);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(refreshKey),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                ClaimsPrincipal principal;
                try
                {
                    principal = tokenHandler.ValidateToken(refreshToken, validationParameters, out var validatedToken);

                    // Verify this is a refresh token
                    var tokenType = principal.Claims.FirstOrDefault(x => x.Type == "tokenType")?.Value;
                    if (tokenType != "refresh")
                    {
                        throw new SecurityTokenException("Invalid token type.");
                    }
                }
                catch (Exception)
                {
                    throw new SecurityTokenException("Invalid refresh token.");
                }

                // Get user information from claims
                var userId = int.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? throw new SecurityTokenException("Invalid token claims."));

                // Get user from repository
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    throw new SecurityTokenException("User not found.");
                }

                // Generate new tokens
                var newAccessToken = GenerateJwtToken(user, false);
                var newRefreshToken = GenerateJwtToken(user, true);

                return new TokenDto
                {
                    Token = newAccessToken,
                    RefreshToken = newRefreshToken
                };
            }
            catch (Exception ex)
            {
                throw new SecurityTokenException("Error processing refresh token.", ex);
            }
        }

        private static bool VerifyPassword(User user, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }
    }
}