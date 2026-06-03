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
                if (db.Applications.Any(a =>
                    a.GameSessionId == application.GameSessionId &&
                    a.PlayerId == application.PlayerId &&
                    a.Status != ApplicationStatus.Rejected))
                    return null;

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

                // Если отклоняем — удаляем запись чтобы игрок мог подать снова
                if (application.Status == ApplicationStatus.Rejected)
                {
                    var deleted = new ApplicationData()
                    {
                        Id = app.Id,
                        GameSessionId = app.GameSessionId,
                        PlayerId = app.PlayerId,
                        Status = ApplicationStatus.Rejected,
                        Message = app.Message,
                        CreatedAt = app.CreatedAt,
                        UpdatedAt = DateTime.Now,
                    };
                    db.Applications.Remove(app);
                    db.SaveChanges();
                    return deleted;
                }

                app.Status = application.Status;
                app.UpdatedAt = DateTime.Now;
                db.SaveChanges();
                return app;
            }
        }

        public ApplicationData? ExecuteUpdateStatusAction(int id, int status)
        {
            using (var db = new DatabaseContext())
            {
                // 1. Ищем заявку
                var app = db.Applications.FirstOrDefault(a => a.Id == id);
                if (app == null) return null;

                // 2. Если мы пытаемся одобрить игрока, давай проверим лимит еще раз (на всякий случай)
                if (status == (int)ApplicationStatus.Approved)
                {
                    var session = db.GameSessions.FirstOrDefault(s => s.Id == app.GameSessionId);
                    var approvedCount = db.Applications.Count(a =>
                        a.GameSessionId == app.GameSessionId &&
                        a.Status == ApplicationStatus.Approved);

                    if (session != null && approvedCount >= session.MaxPlayers)
                    {
                        // Можно либо вернуть null, либо выбросить исключение, 
                        // что мест больше нет
                        return null;
                    }
                }

                // 3. Обновляем статус и время
                app.Status = (ApplicationStatus)status;
                app.UpdatedAt = DateTime.Now;

                db.SaveChanges();
                return app;
            }
        }

    }
}