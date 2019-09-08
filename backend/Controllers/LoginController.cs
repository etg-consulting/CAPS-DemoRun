using Microsoft.AspNetCore.Mvc;
using Caps.Services;
using Caps.Models;

namespace Caps.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]Login loginParam)
        {
            var user = _userService.Authenticate(loginParam.Username, loginParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }
    }
}