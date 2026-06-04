using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orichalcum.BusinessLogic.Core.UserReview;
using Orichalcum.BusinessLogic.Interface;
using Orichalcum.Domains.Entities.UserReview;

namespace Orichalcum.BusinessLogic.Functions.UserReview
{
    public class UserReviewFlow : UserReviewActions, IUserReviewActions
    {
        public List<UserReviewData> GetReviewsByUserAction(int targetUserId) =>
            ExecuteGetReviewsByUserAction(targetUserId);

        public UserReviewData? CreateOrUpdateReviewAction(UserReviewData review) =>
            ExecuteCreateOrUpdateReviewAction(review);

        public bool DeleteReviewAction(int authorId, int targetId) =>
            ExecuteDeleteReviewAction(authorId, targetId);
    }
}