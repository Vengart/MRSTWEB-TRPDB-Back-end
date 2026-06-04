using Orichalcum.BusinessLogic.Interface;
using Orichalcum.Domains.Entities.UserReview;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Orichalcum.Api.Controller
{
    [Route("api/reviews")]
    [ApiController]
    public class UserReviewController : ControllerBase
    {
        public IUserReviewActions _reviewActions;

        public UserReviewController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _reviewActions = bl.GetUserReviewActions();
        }

        [AllowAnonymous]
        [HttpGet("user/{targetUserId}")]
        public IActionResult GetReviews(int targetUserId)
        {
            var reviews = _reviewActions.GetReviewsByUserAction(targetUserId);
            return Ok(new
            {
                likes = reviews.Count(r => r.IsLike),
                dislikes = reviews.Count(r => !r.IsLike),
                reviews = reviews.Select(r => new {
                    id = r.Id,
                    isLike = r.IsLike,
                    comment = r.Comment,
                    authorUserId = r.AuthorUserId
                })
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateOrUpdateReview([FromBody] UserReviewData review)
        {
            var authorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (authorId == null) return Unauthorized();

            review.AuthorUserId = int.Parse(authorId);
            var result = _reviewActions.CreateOrUpdateReviewAction(review);
            return Ok(new { removed = result == null, review = result });
        }

        [Authorize]
        [HttpDelete("user/{targetUserId}")]
        public IActionResult DeleteReview(int targetUserId)
        {
            var authorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (authorId == null) return Unauthorized();

            var deleted = _reviewActions.DeleteReviewAction(int.Parse(authorId), targetUserId);
            if (!deleted) return NotFound();
            return Ok(new { Message = "Review deleted" });
        }
    }
}