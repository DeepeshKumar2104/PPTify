using Microsoft.AspNetCore.Mvc;

namespace PPTify.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUserArray()
        {
            // Array define karte hain
            int[] arr = { 1, 2, 3, 4, 5 };

            
            var userList = arr.ToList(); 
            
            return Ok(userList);
        }
    }
}
