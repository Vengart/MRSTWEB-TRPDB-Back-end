using Orichalcum.BusinessLogic.Core.User;
using Orichalcum.BusinessLogic.Interface;
using Orichalcum.Domains.Entities.User;
using Orichalcum.Domains.Models.User;
using Orichalcum.Domains.Models.User;


namespace Orichalcum.BusinessLogic.Functions.User
{
    public class UserFlow : UserActions, IUserActions
    {
        public List<UserDto> GetAllUsersAction()
        {
            var _users = ExecuteGetAllUsersAction();
            return _users.Select(u => ToDto(u)).ToList();
        }

        public UserDto? GetUserByIdAction(int id)
        {
            var _user = ExecuteGetUserByIdAction(id);
            return _user == null ? null : ToDto(_user);
        }

        public UserDto? CreateUserAction(UserData user)
        {
            var _user = ExecuteCreateUserAction(user);
            return _user == null ? null : ToDto(_user);
        }

        public bool DeleteUserAction(int id)
        {
            return ExecuteDeleteUserAction(id);
        }

        public UserDto? UpdateUserAction(int id, UpdateUserDto dto)
        {
            var _user = ExecuteUpdateUserAction(id, dto);
            return _user == null ? null : ToDto(_user);
        }

        public bool HardDeleteUserAction(int id) =>
            ExecuteHardDeleteUserAction(id);

        private UserDto ToDto(UserData u) => new UserDto()
        {
            Id = u.Id,
            UserName = u.UserName,
            Email = u.Email,
            Bio = u.Bio,
            AvatarUrl = u.AvatarUrl,
            Role = u.Role,
            IsActive = u.IsActive,
        };
    }
}