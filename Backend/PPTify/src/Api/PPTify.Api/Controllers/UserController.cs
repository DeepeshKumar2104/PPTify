using Microsoft.AspNetCore.Mvc;
using PPTify.Application.Contracts.DTos;
using PPTify.Application.Contracts.Interface;

namespace PPTify.Api.Controllers
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

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDTo userdto)
        {
            if (userdto == null)
            {
                return BadRequest("User data is null");
            }
            var result = await userService.RegisterUserAsync(userdto);
            if (result)
            {
                return Ok(new {message = "User registered successfully " });
            }
            else if (result == false)
            {
                return BadRequest(new { message = "User registration failed" });
            }
            else
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
