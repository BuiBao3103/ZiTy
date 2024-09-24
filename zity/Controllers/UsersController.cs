using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Get()
        {
            var users = await userService.GetAllAsync();
            return Ok(users);
        }

    }
}
