using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orichalcum.Domains.Entities.UserReview;

namespace Orichalcum.BusinessLogic.Interface
{
    public interface IUserReviewActions
    {
        List<UserReviewData> GetReviewsByUserAction(int targetUserId);
        UserReviewData? CreateOrUpdateReviewAction(UserReviewData review);
        bool DeleteReviewAction(int authorId, int targetId);
    }
}