using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orichalcum.BusinessLogic.Core.Auth;
using Orichalcum.BusinessLogic.Interface;
using Orichalcum.Domains.Models.User;

namespace Orichalcum.BusinessLogic.Functions.Auth
{
    public class AuthFlow : AuthActions, IAuthActions
    {
        private readonly TokenService _tokenService = new TokenService();

        public object? LoginActionFlow(UserAuthAction auth)
        {
            var user = ValidateLogin(auth);
            if (user == null) return null;

            var token = _tokenService.GenerateToken(
                user.Id,
                user.UserName ?? "",
                user.Role.ToString()
            );

            return new { Token = token, UserId = user.Id, Role = user.Role };
        }
    }
}