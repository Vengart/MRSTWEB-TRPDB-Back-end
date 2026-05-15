using Orichalcum.BusinessLogic.Interface;
using Orichalcum.Domains.Entities.GameNote;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Orichalcum.Api.Controller
{
    [Route("api/gamenotes")]
    [ApiController]
    public class GameNoteController : ControllerBase
    {
        public IGameNoteActions _gameNoteActions;

        public GameNoteController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _gameNoteActions = bl.GetGameNoteActions();
        }

        [Authorize]
        [HttpGet("card/{cardId}")]
        public IActionResult GetNotesByCard(int cardId)
        {
            var notes = _gameNoteActions.GetNotesByCardAction(cardId);
            return Ok(notes);
        }

        [Authorize(Roles = "GameMaster,Admin")]
        [HttpPost]
        public IActionResult CreateNote([FromBody] GameNoteData note)
        {
            var created = _gameNoteActions.CreateNoteAction(note);
            if (created == null) return BadRequest();
            return Ok(created);
        }

        [Authorize(Roles = "GameMaster,Admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateNote(int id, [FromBody] GameNoteData note)
        {
            var updated = _gameNoteActions.UpdateNoteAction(id, note);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            var deleted = _gameNoteActions.DeleteNoteAction(id);
            if (!deleted) return NotFound();
            return Ok(new { Message = "Note deleted" });
        }
    }
}