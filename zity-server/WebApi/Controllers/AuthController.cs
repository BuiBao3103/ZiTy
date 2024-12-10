using Application.DTOs.Auth;
using Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace WebApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await _authService.AuthenticateAsync(loginDto);
        if (result == null)
            return Unauthorized(new { message = "Invalid username or password." });
        return Ok(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
    {
        try
        {
            if (string.IsNullOrEmpty(refreshTokenDto.RefreshToken))
                return BadRequest(new { message = "Refresh token is required." });

            var result = await _authService.RefreshTokenAsync(refreshTokenDto.RefreshToken);
            return Ok(result);
        }
        catch (SecurityTokenException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPatch("first-login/update-password")]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordFirstLoginDTO updatePasswordDto)
    {

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized(new { message = "Invalid token" });

        var userId = int.Parse(userIdClaim.Value);
        await _authService.UpdatePasswordFirstLoginAsync(userId, updatePasswordDto);
        return Ok();
    }

}