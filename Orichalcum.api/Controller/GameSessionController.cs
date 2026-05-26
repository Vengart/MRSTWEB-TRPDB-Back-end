using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orichalcum.BusinessLogic.Interface;
using Orichalcum.Domains.Entities.GameSession;

namespace Orichalcum.Api.Controller
{
    [Route("api/gamesessions")]
    [ApiController]
    public class GameSessionController : ControllerBase
    {
        public IGameSessionActions _gameSessionActions;

        public GameSessionController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _gameSessionActions = bl.GetGameSessionActions();
        }

        [HttpGet]
        public IActionResult GetAllSessions()
        {
            var sessions = _gameSessionActions.GetAllSessionsAction();
            return Ok(sessions);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetSessionById(int id)
        {
            var session = _gameSessionActions.GetSessionByIdAction(id);
            if (session == null) return NotFound();

            return Ok(new
            {
                id = session.Id,
                title = session.Title,
                description = session.Description,
                system = session.System,
                setting = session.Setting,
                maxPlayers = session.MaxPlayers,
                coverImageUrl = session.CoverImageUrl,
                duration = session.Duration,
                price = session.Price,
                status = session.Status,
                scheduledAt = session.ScheduledAt,
                gameMasterId = session.GameMasterId,
                gameCardId = session.GameCardId,
                applications = session.Applications?.Select(a => new {
                    id = a.Id,
                    status = a.Status,
                    message = a.Message,
                    playerId = a.PlayerId,
                    player = a.Player == null ? null : new
                    {
                        id = a.Player.Id,
                        userName = a.Player.UserName,
                        avatarUrl = a.Player.AvatarUrl,
                        bio = a.Player.Bio
                    }
                })
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateSession([FromBody] GameSessionData session)
        {
            var created = _gameSessionActions.CreateSessionAction(session);
            if (created == null) return BadRequest();
            return Ok(created);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSession(int id)
        {
            var deleted = _gameSessionActions.DeleteSessionAction(id);
            if (!deleted) return NotFound();
            return Ok(new { Message = "Session deleted" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSession(int id, [FromBody] GameSessionData session)
        {
            var updated = _gameSessionActions.UpdateSessionAction(id, session);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
    }
}