using Microsoft.AspNetCore.Mvc;
using zity.DTOs.Users;
using zity.Services.Interfaces;
namespace zity.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

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

        [HttpPost("{id}/avatar")]
        public async Task<IActionResult> UploadAvatarAsync([FromRoute] int id, IFormFile file)
        {
            return Ok(await _userService.UploadAvatarAsync(id, file));
        }
    }


}
