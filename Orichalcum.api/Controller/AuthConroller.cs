using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orichalcum.BusinessLogic.Interface;
using Orichalcum.Domains.Models.User;

namespace Orichalcum.Api.Controller
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IAuthActions _authActions;

        public AuthController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _authActions = bl.GetAuthActions();
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok(new { Message = "Auth is active" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserAuthAction data)
        {
            var result = _authActions.LoginActionFlow(data);
            if (result == null) return Unauthorized(new { Message = "Invalid login or password" });
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserAuthAction data)
        {
            var bl = new BusinessLogic.BusinessLogic();
            var userActions = bl.GetUserActions();

            var newUser = new Orichalcum.Domains.Entities.User.UserData()
            {
                UserName = data.Login,
                Password = data.Password,
                Email = data.Email ?? "",
                Role = Orichalcum.Domains.Enums.UserRole.Player,
                IsActive = true,
            };

            var created = userActions.CreateUserAction(newUser);
            if (created == null) return BadRequest(new { Message = "User already exists" });
            return Ok(new { Message = "Registered successfully", UserId = created.Id });
        }

    }
}