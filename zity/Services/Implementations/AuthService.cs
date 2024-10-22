using Microsoft.AspNetCore.Identity;
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
    public class AuthService(IUserRepository userRepository, JWTSettings jwtSettings) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly JWTSettings _jwtSettings = jwtSettings;

        public async Task<TokenDto?> AuthenticateAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByUsernameAsync(loginDto.Username);
            if (user == null || !VerifyPassword(user, loginDto.Password))
                return null;

            var token = GenerateJwtToken(user);

            await _userRepository.UpdateAsync(user);

            return new TokenDto { Token = token };
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

        private static bool VerifyPassword(User user, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }
    }
}
