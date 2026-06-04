using Orichalcum.BusinessLogic.Core.Application;
using Orichalcum.BusinessLogic.Functions.Application;
using Orichalcum.BusinessLogic.Functions.Auth;
using Orichalcum.BusinessLogic.Functions.GameCard;
using Orichalcum.BusinessLogic.Functions.GameCard;
using Orichalcum.BusinessLogic.Functions.GameNote;
using Orichalcum.BusinessLogic.Functions.GameNote;
using Orichalcum.BusinessLogic.Functions.GameSession;
using Orichalcum.BusinessLogic.Functions.User;
using Orichalcum.BusinessLogic.Interface;
using Orichalcum.DataAccess.Context;
using Orichalcum.Domains.Entities.Application;
using Orichalcum.BusinessLogic.Functions.UserReview;



namespace Orichalcum.BusinessLogic
{
    public class BusinessLogic
    {
        public BusinessLogic() { }

        public IAuthActions GetAuthActions()
        {
            return new AuthFlow();
        }

        public IUserActions GetUserActions()
        {
            return new UserFlow();
        }

        public IGameSessionActions GetGameSessionActions()
        {
            return new GameSessionFlow();
        }
        public IApplicationActions GetApplicationActions()
        {
            return new ApplicationFlow();
        }

        public IGameCardActions GetGameCardActions()
        {
            return new GameCardFlow();
        }
        public IGameNoteActions GetGameNoteActions()
        {
            return new GameNoteFlow();
        }

        public IUserReviewActions GetUserReviewActions()
        {
            return new UserReviewFlow();
        }

        public ApplicationData? UpdateStatusAction(int id, int status) =>
            new ApplicationActions().ExecuteUpdateStatusAction(id, status);
    }
}