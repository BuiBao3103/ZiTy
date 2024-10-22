using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using zity.DTOs.Auth;
using zity.Services.Interfaces;

namespace zity.Controllers
{
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
            try
            {
                var result = await _authService.AuthenticateAsync(loginDto);
                if (result == null)
                    return Unauthorized(new { message = "Invalid username or password." });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", error = ex.Message });
            }
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
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", error = ex.Message });
            }
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenDto revokeTokenDto)
        {
            try
            {
                if (string.IsNullOrEmpty(revokeTokenDto.RefreshToken))
                    return BadRequest(new { message = "Refresh token is required." });

                await _authService.RevokeRefreshTokenAsync(revokeTokenDto.RefreshToken);
                return Ok(new { message = "Token revoked successfully." });
            }
            catch (SecurityTokenException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", error = ex.Message });
            }
        }
    }
}