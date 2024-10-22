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

        public async Task<TokenDto> AuthenticateAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByUsernameAsync(loginDto.Username);
            if (user == null || !VerifyPassword(user, loginDto.Password))
                return null;

            var token = GenerateJwtToken(user);
            //var refreshToken = GenerateRefreshToken();

            //user.RefreshTokens.Add(refreshToken);
            await _userRepository.UpdateAsync(user);
            //return new TokenDto { Token = token, RefreshToken = refreshToken.Token };

            return new TokenDto { Token = token };
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.UserType),
                //new Claim("CanAccessSensitiveData", user.CanAccessSensitiveData.ToString())
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

        //public RefreshToken GenerateRefreshToken()
        //{
        //    var randomNumber = new byte[32];
        //    using var rng = RandomNumberGenerator.Create();
        //    rng.GetBytes(randomNumber);
        //    return new RefreshToken
        //    {
        //        Token = Convert.ToBase64String(randomNumber),
        //        ExpiryDate = DateTime.UtcNow.AddDays(7)
        //    };
        //}

        //public async Task<TokenDto> RefreshTokenAsync(string token, string refreshToken)
        //{
        //    var principal = GetPrincipalFromExpiredToken(token);
        //    var username = principal.Identity.Name;

        //    var user = await _userRepository.GetByUsernameAsync(username);
        //    if (user == null)
        //        return null;

        //    //var storedRefreshToken = user.RefreshTokens.SingleOrDefault(r => r.Token == refreshToken);

        //    //if (storedRefreshToken == null || storedRefreshToken.ExpiryDate <= DateTime.UtcNow)
        //    //    return null;

        //    var newToken = GenerateJwtToken(user);
        //    //var newRefreshToken = GenerateRefreshToken();

        //    //user.RefreshTokens.Remove(storedRefreshToken);
        //    //user.RefreshTokens.Add(newRefreshToken);
        //    await _userRepository.UpdateAsync(user);

        //    //return new TokenDto { Token = newToken, RefreshToken = newRefreshToken.Token };
        //    return new TokenDto { Token = newToken};
        //}

        //public async Task<bool> RevokeTokenAsync(string username)
        //{
        //    var user = await _userRepository.GetByUsernameAsync(username);
        //    if (user == null)
        //        return false;

        //    user.RefreshTokens.Clear();
        //    await _userRepository.UpdateAsync(user);

        //    return true;
        //}

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        private static bool VerifyPassword(User user, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }
    }
}
