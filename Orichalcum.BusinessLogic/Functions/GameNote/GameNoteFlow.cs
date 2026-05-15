using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orichalcum.BusinessLogic.Core.GameNote;
using Orichalcum.BusinessLogic.Interface;
using Orichalcum.Domains.Entities.GameNote;

namespace Orichalcum.BusinessLogic.Functions.GameNote
{
    public class GameNoteFlow : GameNoteActions, IGameNoteActions
    {
        public List<GameNoteData> GetNotesByCardAction(int cardId) =>
            ExecuteGetNotesByCardAction(cardId);
        public GameNoteData? CreateNoteAction(GameNoteData note) =>
            ExecuteCreateNoteAction(note);
        public bool DeleteNoteAction(int id) =>
            ExecuteDeleteNoteAction(id);
        public GameNoteData? UpdateNoteAction(int id, GameNoteData note) =>
            ExecuteUpdateNoteAction(id, note);
    }
}