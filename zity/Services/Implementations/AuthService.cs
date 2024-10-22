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
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly JWTSettings _jwtSettings;

        public AuthService(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, JWTSettings jwtSettings)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtSettings = jwtSettings;
        }

        public async Task<TokenDto?> AuthenticateAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByUsernameAsync(loginDto.Username);
            if (user == null || !VerifyPassword(user, loginDto.Password))
                return null;

            return await GenerateTokensAsync(user);
        }

        private async Task<TokenDto> GenerateTokensAsync(User user)
        {
            var accessToken = GenerateJwtToken(user, false);
            var refreshToken = GenerateJwtToken(user, true);

            // Hash the refresh token before saving
            var hashedRefreshToken = HashToken(refreshToken);

            // Save hashed refresh token to database
            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                Token = hashedRefreshToken,
                ExpiryTime = DateTime.UtcNow.AddDays(30),
                UserId = user.Id,
                IsRevoked = false
            });

            // Return original tokens to client
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

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: isRefreshToken
                    ? DateTime.UtcNow.AddDays(30)
                    : DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<TokenDto> RefreshTokenAsync(string refreshToken)
        {
            try
            {
                // Validate refresh token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
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

                // Get user ID from claims
                var userId = int.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? throw new SecurityTokenException("Invalid token claims."));

                // Find all non-revoked refresh tokens for this user
                var userTokens = await _refreshTokenRepository.GetUserRefreshTokensAsync(userId);
                var validStoredToken = userTokens
                    .Where(t => !t.IsRevoked && t.ExpiryTime > DateTime.UtcNow)
                    .FirstOrDefault(t => VerifyToken(refreshToken, t.Token));

                if (validStoredToken == null)
                {
                    throw new SecurityTokenException("Invalid or expired refresh token.");
                }

                // Get user
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    throw new SecurityTokenException("User not found.");
                }

                // Revoke the old refresh token
                await _refreshTokenRepository.RevokeAsync(validStoredToken);

                // Generate new tokens
                return await GenerateTokensAsync(user);
            }
            catch (Exception ex)
            {
                throw new SecurityTokenException("Error processing refresh token.", ex);
            }
        }

        public async Task RevokeRefreshTokenAsync(string refreshToken)
        {
            // Find token by comparing hashes
            var userTokens = await _refreshTokenRepository.GetAllActiveTokensAsync();
            var storedToken = userTokens.FirstOrDefault(t => VerifyToken(refreshToken, t.Token));

            if (storedToken == null)
            {
                throw new SecurityTokenException("Token not found.");
            }

            await _refreshTokenRepository.RevokeAsync(storedToken);
        }

        private static bool VerifyPassword(User user, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

        private static string HashToken(string token)
        {
            return BCrypt.Net.BCrypt.HashPassword(token, BCrypt.Net.BCrypt.GenerateSalt());
        }

        private static bool VerifyToken(string providedToken, string hashedToken)
        {
            return BCrypt.Net.BCrypt.Verify(providedToken, hashedToken);
        }
    }
}