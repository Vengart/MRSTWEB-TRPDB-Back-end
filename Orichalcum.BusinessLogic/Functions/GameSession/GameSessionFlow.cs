using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orichalcum.BusinessLogic.Core.GameSession;
using Orichalcum.BusinessLogic.Interface;
using Orichalcum.Domains.Entities.GameSession;

namespace Orichalcum.BusinessLogic.Functions.GameSession
{
    public class GameSessionFlow : GameSessionActions, IGameSessionActions
    {
        public List<GameSessionData> GetAllSessionsAction() =>
            ExecuteGetAllSessionsAction();

        public GameSessionData? GetSessionByIdAction(int id) =>
            ExecuteGetSessionByIdAction(id);

        public GameSessionData? CreateSessionAction(GameSessionData session) =>
            ExecuteCreateSessionAction(session);

        public bool DeleteSessionAction(int id) =>
            ExecuteDeleteSessionAction(id);

        public GameSessionData? UpdateSessionAction(int id, GameSessionData session) =>
            ExecuteUpdateSessionAction(id, session);

        public List<GameSessionData> GetSessionsByUserIdAction(int userId) =>
            ExecuteGetSessionsByUserIdAction(userId);
    }
}