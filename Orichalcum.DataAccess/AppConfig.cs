using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orichalcum.DataAccess
{
    public class AppConfig
    {
        public static string JwtKey { get; set; } = "";
        public static string JwtIssuer { get; set; } = "";
        public static string JwtAudience { get; set; } = "";
    }
}