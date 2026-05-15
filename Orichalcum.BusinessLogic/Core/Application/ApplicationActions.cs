using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orichalcum.DataAccess.Context;
using Orichalcum.Domains.Entities.Application;
using Orichalcum.Domains.Enums;

namespace Orichalcum.BusinessLogic.Core.Application
{
    public class ApplicationActions
    {
        public List<ApplicationData> ExecuteGetApplicationsBySessionAction(int sessionId)
        {
            using (var db = new DatabaseContext())
            {
                return db.Applications
                    .Where(a => a.GameSessionId == sessionId)
                    .ToList();
            }
        }

        public ApplicationData? ExecuteCreateApplicationAction(ApplicationData application)
        {
            using (var db = new DatabaseContext())
            {
                // Проверяем что такой заявки ещё нет
                if (db.Applications.Any(a =>
                    a.GameSessionId == application.GameSessionId &&
                    a.PlayerId == application.PlayerId))
                    return null;

                // Проверяем лимит игроков
                var session = db.GameSessions.FirstOrDefault(s => s.Id == application.GameSessionId);
                if (session == null) return null;
                var approvedCount = db.Applications.Count(a =>
                    a.GameSessionId == application.GameSessionId &&
                    a.Status == ApplicationStatus.Approved);
                if (approvedCount >= session.MaxPlayers) return null;

                var _new = new ApplicationData()
                {
                    Message = application.Message,
                    Status = ApplicationStatus.Pending,
                    GameSessionId = application.GameSessionId,
                    PlayerId = application.PlayerId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                db.Applications.Add(_new);
                db.SaveChanges();
                return _new;
            }
        }

        public bool ExecuteDeleteApplicationAction(int id)
        {
            using (var db = new DatabaseContext())
            {
                var app = db.Applications.FirstOrDefault(a => a.Id == id);
                if (app == null) return false;
                db.Applications.Remove(app);
                db.SaveChanges();
                return true;
            }
        }

        public ApplicationData? ExecuteUpdateApplicationAction(int id, ApplicationData application)
        {
            using (var db = new DatabaseContext())
            {
                var app = db.Applications.FirstOrDefault(a => a.Id == id);
                if (app == null) return null;
                app.Status = application.Status;
                app.UpdatedAt = DateTime.Now;
                db.SaveChanges();
                return app;
            }
        }
    }
}