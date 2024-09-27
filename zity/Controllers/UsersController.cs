using Microsoft.AspNetCore.Mvc;
using zity.DTOs.Users;
using zity.Services.Interfaces;
namespace zity.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] UserQueryDto query)
        {
            return Ok(await userService.GetAllAsync(query));
        }

    }
}
