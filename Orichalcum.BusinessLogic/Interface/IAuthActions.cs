using Orichalcum.Domains.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orichalcum.BusinessLogic.Interface
{
    public interface IAuthActions
    {
        object? LoginActionFlow(UserAuthAction auth);
    }
}
