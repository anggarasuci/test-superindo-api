using Microsoft.AspNetCore.Mvc;
using ProductApi.Data.Entity;
using ProductApi.Data.Service;

namespace ProductApi.Domain.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{username}/{password}")]
        public async Task<ActionResult<UserEntity>> Get(string username, string password)
        {
            var result = await userService.LoginUserAsync(username, password);
            if (result is null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserEntity user)
        {
            await userService.AddUserAsync(user);
            return CreatedAtAction(nameof(Get), new
            {
                id = user.Id
            }, user);
        }
    }
}

