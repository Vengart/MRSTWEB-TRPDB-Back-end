using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orichalcum.DataAccess.Context;
using Orichalcum.Domains.Entities.UserReview;

namespace Orichalcum.BusinessLogic.Core.UserReview
{
    public class UserReviewActions
    {
        public List<UserReviewData> ExecuteGetReviewsByUserAction(int targetUserId)
        {
            using (var db = new DatabaseContext())
                return db.UserReviews
                    .Where(r => r.TargetUserId == targetUserId)
                    .ToList();
        }

        public UserReviewData? ExecuteCreateOrUpdateReviewAction(UserReviewData review)
        {
            using (var db = new DatabaseContext())
            {
                // Нельзя оценить себя
                if (review.AuthorUserId == review.TargetUserId) return null;

                var existing = db.UserReviews.FirstOrDefault(r =>
                    r.TargetUserId == review.TargetUserId &&
                    r.AuthorUserId == review.AuthorUserId);

                if (existing != null)
                {
                    // Если та же оценка — удаляем (toggle)
                    if (existing.IsLike == review.IsLike)
                    {
                        db.UserReviews.Remove(existing);
                        db.SaveChanges();
                        return null;
                    }
                    // Если другая оценка — обновляем
                    existing.IsLike = review.IsLike;
                    existing.Comment = review.Comment;
                    existing.UpdatedAt = DateTime.Now;
                    db.SaveChanges();
                    return existing;
                }

                var _new = new UserReviewData()
                {
                    IsLike = review.IsLike,
                    Comment = review.Comment,
                    TargetUserId = review.TargetUserId,
                    AuthorUserId = review.AuthorUserId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                db.UserReviews.Add(_new);
                db.SaveChanges();
                return _new;
            }
        }

        public bool ExecuteDeleteReviewAction(int authorId, int targetId)
        {
            using (var db = new DatabaseContext())
            {
                var review = db.UserReviews.FirstOrDefault(r =>
                    r.AuthorUserId == authorId && r.TargetUserId == targetId);
                if (review == null) return false;
                db.UserReviews.Remove(review);
                db.SaveChanges();
                return true;
            }
        }
    }
}