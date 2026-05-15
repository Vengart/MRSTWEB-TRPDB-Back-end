using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orichalcum.Domains.Entities.Application;

namespace Orichalcum.BusinessLogic.Interface
{
    public interface IApplicationActions
    {
        List<ApplicationData> GetApplicationsBySessionAction(int sessionId);
        ApplicationData? CreateApplicationAction(ApplicationData application);
        bool DeleteApplicationAction(int id);
        ApplicationData? UpdateApplicationAction(int id, ApplicationData application);

        ApplicationData UpdateStatusAction(int id, int status);
    }
}