using Microsoft.AspNetCore.Mvc;
using ZiTy.DTOs.Users;
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
            return Ok(await userService.GetAllAsync(query));
        }

    }
}
