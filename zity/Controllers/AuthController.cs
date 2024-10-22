using Microsoft.AspNetCore.Mvc;
using zity.DTOs.Auth;
using zity.Services.Interfaces;

namespace zity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authService.AuthenticateAsync(loginDto);
            if (result == null)
                return Unauthorized();

            return Ok(result);
        }
    }
}
