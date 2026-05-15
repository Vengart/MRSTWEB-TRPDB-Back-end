using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orichalcum.BusinessLogic.Interface;
using Orichalcum.Domains.Entities.User;
using Orichalcum.Domains.Models.User;

namespace Orichalcum.Api.Controller
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserActions _userActions;

        public UserController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _userActions = bl.GetUserActions();
        }

        // Только Admin видит всех пользователей
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userActions.GetAllUsersAction();
            return Ok(users);
        }



        // Авторизованный пользователь видит профиль по id
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userActions.GetUserByIdAction(id);
            if (user == null) return NotFound();
            return Ok(new
            {
                id = user.Id,
                userName = user.UserName,
                email = user.Email,
                bio = user.Bio,
                avatarUrl = user.AvatarUrl,
                firstName = user.FirstName,
                lastName = user.LastName,
                role = user.Role
            });
        }

        // Только Admin может удалять пользователей
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var deleted = _userActions.DeleteUserAction(id);
            if (!deleted) return NotFound();
            return Ok(new { Message = "User deleted" });
        }


        // Пользователь обновляет свой профиль
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UpdateUserDto dto)
        {
            var updated = _userActions.UpdateUserAction(id, dto);
            if (updated == null) return NotFound();
            return Ok(new
            {
                id = updated.Id,
                userName = updated.UserName,
                email = updated.Email,
                bio = updated.Bio,
                avatarUrl = updated.AvatarUrl,
                firstName = updated.FirstName,
                lastName = updated.LastName,
                role = updated.Role
            });
        }
    }
}