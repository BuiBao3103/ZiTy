using Microsoft.AspNetCore.Mvc;
using zity.DTOs.Users;
using ZiTy.Services.Interfaces;
namespace ZiTy.Controllers
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
            var users = await userService.GetAllAsync(query);
            return Ok(users);
        }

    }
}
