using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orichalcum.Domains.Models.User
{
    public class UserAuthAction
    {
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
    }
}