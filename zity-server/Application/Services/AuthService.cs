﻿using Application.DTOs.Auth;
using Application.Interfaces;
using Domain.Configurations;
using Domain.Core.Repositories;
using Domain.Core.Specifications;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services;

public class AuthService(IUnitOfWork unitOfWork, AppSettings appSettings) : IAuthService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly AppSettings _appSettings = appSettings;

    public async Task<TokenDto?> AuthenticateAsync(LoginDto loginDto)
    {
        var spec = new BaseSpecification<User>(u => u.Username == loginDto.Username);
        var user = await _unitOfWork.Repository<User>().FirstOrDefaultAsync(spec);
        if (user == null || !VerifyPassword(user, loginDto.Password))
            return null;

        //CHECK IF OWNER HAS A BILL IS OVERDUE
        if (user.UserType == "OWNER")
        {
            var specBill = new BaseSpecification<Bill>(b => b.Relationship.UserId == user.Id && b.Status == "OVERDUE");
            var bill = await _unitOfWork.Repository<Bill>().FirstOrDefaultAsync(specBill);
            if (bill != null)
                throw new BusinessRuleException("You have an overdue bill. Please pay before login.");
        }

        var specRelationship = new BaseSpecification<Relationship>(r => r.UserId == user.Id);
        specRelationship.AddInclude(r => r.Apartment);
        var relationships = await _unitOfWork.Repository<Relationship>().ListAsync(specRelationship);

        foreach (var relationship in relationships)
        {
            if (relationship.Apartment.Status == "DISCRUPTION")
                throw new BusinessRuleException("Your apartment is in discruption. Please contact the admin for more information.");
        }


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

        var secretKey = isRefreshToken ? _appSettings.JWTSettings.RefreshTokenKey : _appSettings.JWTSettings.AccessTokenKey;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _appSettings.JWTSettings.Issuer,
            audience: _appSettings.JWTSettings.Audience,
            claims: claims,
            expires: isRefreshToken
                ? DateTime.UtcNow.AddDays(_appSettings.JWTSettings.RefreshExpirationInDays)
                : DateTime.UtcNow.AddMinutes(_appSettings.JWTSettings.AccessExpirationInMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<TokenDto> RefreshTokenAsync(string refreshToken)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var refreshKey = Encoding.UTF8.GetBytes(_appSettings.JWTSettings.RefreshTokenKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(refreshKey),
                ValidateIssuer = true,
                ValidIssuer = _appSettings.JWTSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _appSettings.JWTSettings.Audience,
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
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(userId);
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

    public async Task UpdatePasswordFirstLoginAsync(int userId, UpdatePasswordFirstLoginDTO updatePasswordFirstLogin)
    {
        User user = await _unitOfWork.Repository<User>().GetByIdAsync(userId)
            ?? throw new SecurityTokenException("User not found.");
        if (user.IsFirstLogin == false)
            throw new BusinessRuleException("User has already updated password.");
        if (updatePasswordFirstLogin.NewPassword != updatePasswordFirstLogin.ConfirmPassword)
            throw new BusinessRuleException("New password and confirm password do not match.");
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(updatePasswordFirstLogin.NewPassword);
        user.Password = hashedPassword;
        user.IsFirstLogin = false;
        _unitOfWork.Repository<User>().Update(user);
        await _unitOfWork.SaveChangesAsync();
    }
}