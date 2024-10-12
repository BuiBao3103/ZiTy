using Microsoft.AspNetCore.Mvc;
using zity.DTOs.Users;
using zity.Models;
using zity.Services.Interfaces;
namespace zity.Controllers
{
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
            var user = await _userService.GetByIdAsync(id, includes);
            return user == null ? NotFound() : Ok(user);
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
    }


}
