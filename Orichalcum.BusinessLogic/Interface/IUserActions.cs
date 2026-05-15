using Orichalcum.Domains.Entities.User;
using Orichalcum.Domains.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orichalcum.Domains.Models.User;


namespace Orichalcum.BusinessLogic.Interface
{
    public interface IUserActions
    {
        List<UserDto> GetAllUsersAction();

        UserDto? GetUserByIdAction(int id);

        UserDto? CreateUserAction(UserData user);

        bool DeleteUserAction(int id);

        //UserDto? UpdateUserAction(int id, UserData user);

        UserDto? UpdateUserAction(int id, UpdateUserDto dto);

    }
}
