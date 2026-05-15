using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Orichalcum.DataAccess;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Orichalcum.DataAccess;

namespace Orichalcum.BusinessLogic.Core.Auth
{
    public class TokenService
    {
        public string GenerateToken(int userId, string userName, string role)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(AppConfig.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role),
            };

            var token = new JwtSecurityToken(
                issuer: AppConfig.JwtIssuer,
                audience: AppConfig.JwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}