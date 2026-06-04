using Orichalcum.Domains.Entities.User;
using Orichalcum.Domains.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orichalcum.DataAccess.Context;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Orichalcum.BusinessLogic.Core.Auth
{
    public class AuthActions
    {
        public UserData? ValidateLogin(UserAuthAction data)
        {
            if (string.IsNullOrEmpty(data.Login) || string.IsNullOrEmpty(data.Password))
                return null;

            using (var db = new DatabaseContext())
            {
                var user = db.Users.FirstOrDefault(u =>
                    (u.UserName == data.Login || u.Email == data.Login) &&
                    u.IsActive == true);

                if (user == null) return null;

                // Проверяем хеш
                if (!BCrypt.Net.BCrypt.Verify(data.Password, user.Password))
                    return null;

                return user;
            }
        }

    }
}