using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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

            var token = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            // Hash the refresh token before saving it
            var hashedRefreshToken = HashRefreshToken(refreshToken);
            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                Token = hashedRefreshToken,
                ExpiryTime = DateTime.UtcNow.AddDays(30),
                UserId = user.Id
            });

            return new TokenDto { Token = token, RefreshToken = refreshToken };
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.Role, user.UserType),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            // Generate a random refresh token
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task<TokenDto> RefreshTokenAsync(string refreshToken)
        {
            var existingToken = await _refreshTokenRepository.GetByValueAsync(refreshToken);

            if (existingToken == null || existingToken.IsRevoked || existingToken.ExpiryTime < DateTime.UtcNow)
            {
                throw new SecurityTokenException("Invalid refresh token.");
            }

            var user = existingToken.User;
            var newJwtToken = GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken();

            // Revoke the old refresh token
            await _refreshTokenRepository.RevokeAsync(existingToken);

            // Hash and save the new refresh token
            var hashedRefreshToken = HashRefreshToken(newRefreshToken);
            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                Token = hashedRefreshToken,
                ExpiryTime = DateTime.UtcNow.AddDays(30),
                UserId = user.Id
            });

            return new TokenDto { Token = newJwtToken, RefreshToken = newRefreshToken };
        }

        public async Task RevokeRefreshTokenAsync(string refreshToken)
        {
            var existingToken = await _refreshTokenRepository.GetByValueAsync(refreshToken);
            if (existingToken != null)
            {
                await _refreshTokenRepository.RevokeAsync(existingToken);
            }
        }

        private static bool VerifyPassword(User user, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

        private static string HashRefreshToken(string refreshToken)
        {
            // Hash the refresh token using BCrypt
            return BCrypt.Net.BCrypt.HashPassword(refreshToken);
        }
    }
}
