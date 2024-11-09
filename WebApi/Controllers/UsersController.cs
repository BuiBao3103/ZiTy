using Application.Core.Services;
using Application.DTOs.Users;
using Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers;

    [Route("api/users")]
    [ApiController]
    public class UsersController(IUserService userService, IEmailService emailService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IEmailService _emailService = emailService;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] UserQueryDTO query)
        {
            return Ok(await _userService.GetAllAsync(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id, [FromQuery] string? includes)
        {
            return Ok(await _userService.GetByIdAsync(id, includes));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateDTO userCreateDTO)
        {
            var user = await _userService.CreateAsync(userCreateDTO);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPost("{id}/avatar")]
        public async Task<IActionResult> UploadAvatarAsync([FromRoute] int id, IFormFile file)
        {
            return Ok(await _userService.UploadAvatarAsync(id, file));
        }

        [HttpPost("{id}/notify-received-package")]
        public async Task<IActionResult> NotifyReceivedPackage([FromRoute] int id)
        {
            await _userService.NotifyReceivedPackage(id);
            return Ok();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            try
            {
                // Get user ID from JWT token claims
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                    return Unauthorized(new { message = "Invalid token" });

                var userId = int.Parse(userIdClaim.Value);
                var me = await _userService.GetMeAsync(userId);

                if (me == null)
                    return NotFound(new { message = "User not found" });

                return Ok(me);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

