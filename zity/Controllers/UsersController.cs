using Microsoft.AspNetCore.Mvc;
using zity.DTOs.Users;
using zity.Services.Interfaces;
namespace zity.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] UserQueryDTO query)
        {
            return Ok(await _userService.GetAllAsync(query));
        }


    }
}
