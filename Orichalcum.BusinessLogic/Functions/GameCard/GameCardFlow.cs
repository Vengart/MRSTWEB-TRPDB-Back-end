using Orichalcum.BusinessLogic.Core.GameCard;
using Orichalcum.BusinessLogic.Interface;
using Orichalcum.Domains.Entities.GameCard;

namespace Orichalcum.BusinessLogic.Functions.GameCard
{
    public class GameCardFlow : GameCardActions, IGameCardActions
    {
        public List<GameCardData> GetAllCardsAction() =>
            ExecuteGetAllCardsAction();

        public GameCardData? GetCardByIdAction(int id) =>
            ExecuteGetCardByIdAction(id);

        public GameCardData? CreateCardAction(GameCardData card) =>
            ExecuteCreateCardAction(card);

        public bool DeleteCardAction(int id) =>
            ExecuteDeleteCardAction(id);

        public GameCardData? UpdateCardAction(int id, GameCardData card) =>
            ExecuteUpdateCardAction(id, card);
    }
}