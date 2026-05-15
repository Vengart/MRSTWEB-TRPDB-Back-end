using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orichalcum.DataAccess.Context;
using Orichalcum.Domains.Entities.GameCard;

namespace Orichalcum.BusinessLogic.Core.GameCard
{
    public class GameCardActions
    {
        public List<GameCardData> ExecuteGetAllCardsAction()
        {
            using (var db = new DatabaseContext())
            {
                return db.GameCards.ToList();
            }
        }

        public GameCardData? ExecuteGetCardByIdAction(int id)
        {
            using (var db = new DatabaseContext())
            {
                return db.GameCards.FirstOrDefault(x => x.Id == id);
            }
        }

        public GameCardData? ExecuteCreateCardAction(GameCardData card)
        {
            using (var db = new DatabaseContext())
            {
                var _new = new GameCardData()
                {
                    Title = card.Title,
                    Content = card.Content,
                    CoverImageUrl = card.CoverImageUrl,
                    Type = card.Type,
                    IsPublic = card.IsPublic,
                    OwnerId = card.OwnerId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                db.GameCards.Add(_new);
                db.SaveChanges();
                return _new;
            }
        }

        public bool ExecuteDeleteCardAction(int id)
        {
            using (var db = new DatabaseContext())
            {
                var card = db.GameCards.FirstOrDefault(x => x.Id == id);
                if (card == null) return false;
                db.GameCards.Remove(card);
                db.SaveChanges();
                return true;
            }
        }

        public GameCardData? ExecuteUpdateCardAction(int id, GameCardData card)
        {
            using (var db = new DatabaseContext())
            {
                var _card = db.GameCards.FirstOrDefault(x => x.Id == id);
                if (_card == null) return null;
                _card.Title = card.Title;
                _card.Content = card.Content;
                _card.CoverImageUrl = card.CoverImageUrl;
                _card.Type = card.Type;
                _card.IsPublic = card.IsPublic;
                _card.UpdatedAt = DateTime.Now;
                db.SaveChanges();
                return _card;
            }
        }
    }
}