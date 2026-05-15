using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orichalcum.Domains.Entities.GameCard;

namespace Orichalcum.BusinessLogic.Interface
{
    public interface IGameCardActions
    {
        List<GameCardData> GetAllCardsAction();
        GameCardData? GetCardByIdAction(int id);
        GameCardData? CreateCardAction(GameCardData card);
        bool DeleteCardAction(int id);
        GameCardData? UpdateCardAction(int id, GameCardData card);
    }
}