using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orichalcum.Domains.Entities.GameSession;

namespace Orichalcum.BusinessLogic.Interface
{
    public interface IGameSessionActions
    {
        List<GameSessionData> GetAllSessionsAction();
        GameSessionData? GetSessionByIdAction(int id);
        GameSessionData? CreateSessionAction(GameSessionData session);
        bool DeleteSessionAction(int id);
        GameSessionData? UpdateSessionAction(int id, GameSessionData session);
    }
}