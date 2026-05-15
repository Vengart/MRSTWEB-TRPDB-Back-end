using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orichalcum.DataAccess.Context;
using Orichalcum.Domains.Entities.GameNote;

namespace Orichalcum.BusinessLogic.Core.GameNote
{
    public class GameNoteActions
    {
        public List<GameNoteData> ExecuteGetNotesByCardAction(int cardId)
        {
            using (var db = new DatabaseContext())
                return db.GameNotes.Where(n => n.GameCardId == cardId).ToList();
        }

        public GameNoteData? ExecuteCreateNoteAction(GameNoteData note)
        {
            using (var db = new DatabaseContext())
            {
                var _new = new GameNoteData()
                {
                    Header = note.Header,
                    BodyText = note.BodyText,
                    IsVisibleToPlayers = note.IsVisibleToPlayers,
                    GameCardId = note.GameCardId,
                    AuthorId = note.AuthorId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                db.GameNotes.Add(_new);
                db.SaveChanges();
                return _new;
            }
        }

        public bool ExecuteDeleteNoteAction(int id)
        {
            using (var db = new DatabaseContext())
            {
                var note = db.GameNotes.FirstOrDefault(n => n.Id == id);
                if (note == null) return false;
                db.GameNotes.Remove(note);
                db.SaveChanges();
                return true;
            }
        }

        public GameNoteData? ExecuteUpdateNoteAction(int id, GameNoteData note)
        {
            using (var db = new DatabaseContext())
            {
                var _note = db.GameNotes.FirstOrDefault(n => n.Id == id);
                if (_note == null) return null;
                _note.Header = note.Header;
                _note.BodyText = note.BodyText;
                _note.IsVisibleToPlayers = note.IsVisibleToPlayers;
                _note.UpdatedAt = DateTime.Now;
                db.SaveChanges();
                return _note;
            }
        }
    }
}