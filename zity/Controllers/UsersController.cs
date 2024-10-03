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
    }
}
