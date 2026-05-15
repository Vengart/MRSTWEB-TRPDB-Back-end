using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orichalcum.DataAccess.Context;
using Orichalcum.Domains.Entities.GameSession;
using Orichalcum.Domains.Enums;
using Orichalcum.DataAccess;
using Orichalcum.DataAccess.Context;

namespace Orichalcum.BusinessLogic.Core.GameSession
{
    public class GameSessionActions
    {
        public List<GameSessionData> ExecuteGetAllSessionsAction()
        {
            using (var db = new DatabaseContext())
            {
                return db.GameSessions.ToList();
            }
        }

        public GameSessionData? ExecuteGetSessionByIdAction(int id)
        {
            using (var db = new DatabaseContext())
            {
                return db.GameSessions.FirstOrDefault(x => x.Id == id);
            }
        }

        public GameSessionData? ExecuteCreateSessionAction(GameSessionData session)
        {
            using (var db = new DatabaseContext())
            {
                var _newSession = new GameSessionData()
                {
                    Title = session.Title,
                    Description = session.Description,
                    System = session.System,
                    Setting = session.Setting,
                    MaxPlayers = session.MaxPlayers,
                    CoverImageUrl = session.CoverImageUrl,
                    Status = SessionStatus.Open,
                    ScheduledAt = session.ScheduledAt,
                    GameMasterId = session.GameMasterId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                db.GameSessions.Add(_newSession);
                db.SaveChanges();
                return _newSession;
            }
        }

        public bool ExecuteDeleteSessionAction(int id)
        {
            using (var db = new DatabaseContext())
            {
                var session = db.GameSessions.FirstOrDefault(x => x.Id == id);
                if (session == null) return false;
                db.GameSessions.Remove(session);
                db.SaveChanges();
                return true;
            }
        }

        public GameSessionData? ExecuteUpdateSessionAction(int id, GameSessionData session)
        {
            using (var db = new DatabaseContext())
            {
                var _session = db.GameSessions.FirstOrDefault(x => x.Id == id);
                if (_session == null) return null;
                _session.Title = session.Title;
                _session.Description = session.Description;
                _session.System = session.System;
                _session.Setting = session.Setting;
                _session.MaxPlayers = session.MaxPlayers;
                _session.CoverImageUrl = session.CoverImageUrl;
                _session.Status = session.Status;
                _session.ScheduledAt = session.ScheduledAt;
                _session.UpdatedAt = DateTime.Now;
                db.SaveChanges();
                return _session;
            }
        }
    }
}