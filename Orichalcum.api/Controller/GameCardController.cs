using Orichalcum.BusinessLogic.Interface;
using Orichalcum.Domains.Entities.GameCard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Orichalcum.Api.Controller
{
    [Route("api/gamecards")]
    [ApiController]
    public class GameCardController : ControllerBase
    {
        public IGameCardActions _gameCardActions;

        public GameCardController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _gameCardActions = bl.GetGameCardActions();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllCards()
        {
            var cards = _gameCardActions.GetAllCardsAction();
            return Ok(cards);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetCardById(int id)
        {
            var card = _gameCardActions.GetCardByIdAction(id);
            if (card == null) return NotFound();
            return Ok(card);
        }

        [Authorize(Roles = "GameMaster,Admin")]
        [HttpPost]
        public IActionResult CreateCard([FromBody] GameCardData card)
        {
            var created = _gameCardActions.CreateCardAction(card);
            if (created == null) return BadRequest();
            return Ok(created);
        }

        [Authorize(Roles = "GameMaster,Admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateCard(int id, [FromBody] GameCardData card)
        {
            var updated = _gameCardActions.UpdateCardAction(id, card);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCard(int id)
        {
            var deleted = _gameCardActions.DeleteCardAction(id);
            if (!deleted) return NotFound();
            return Ok(new { Message = "Card deleted" });
        }
    }
}