using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orichalcum.BusinessLogic.Core.Application;
using Orichalcum.BusinessLogic.Interface;
using Orichalcum.Domains.Entities.Application;

namespace Orichalcum.BusinessLogic.Functions.Application
{
    public class ApplicationFlow : ApplicationActions, IApplicationActions
    {
        public List<ApplicationData> GetApplicationsBySessionAction(int sessionId) =>
            ExecuteGetApplicationsBySessionAction(sessionId);

        public ApplicationData? CreateApplicationAction(ApplicationData application) =>
            ExecuteCreateApplicationAction(application);

        public bool DeleteApplicationAction(int id) =>
            ExecuteDeleteApplicationAction(id);

        public ApplicationData? UpdateApplicationAction(int id, ApplicationData application) =>
            ExecuteUpdateApplicationAction(id, application);
    }
}