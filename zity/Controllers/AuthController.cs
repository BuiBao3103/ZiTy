using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using zity.DTOs.Auth;
using zity.Services.Interfaces;

namespace zity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
                return Unauthorized();

            return Ok(result);
        }

        //[HttpPost("refresh-token")]
        //public async Task<IActionResult> RefreshToken([FromBody] TokenDto tokenDto)
        //{
        //    var result = await _authService.RefreshTokenAsync(tokenDto.Token, tokenDto.RefreshToken);
        //    if (result == null)
        //        return BadRequest("Invalid client request");

        //    return Ok(result);
        //}

        //[Authorize]
        //[HttpPost("revoke-token")]
        //public async Task<IActionResult> RevokeToken()
        //{
        //    var username = User.Identity.Name;
        //    var result = await _authService.RevokeTokenAsync(username);
        //    if (!result)
        //        return BadRequest("Failed to revoke token");

        //    return Ok();
        //}
    }
}
