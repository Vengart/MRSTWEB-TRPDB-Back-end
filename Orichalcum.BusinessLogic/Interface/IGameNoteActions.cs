using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orichalcum.Domains.Entities.GameNote;

namespace Orichalcum.BusinessLogic.Interface
{
    public interface IGameNoteActions
    {
        List<GameNoteData> GetNotesByCardAction(int cardId);
        GameNoteData? CreateNoteAction(GameNoteData note);
        bool DeleteNoteAction(int id);
        GameNoteData? UpdateNoteAction(int id, GameNoteData note);
    }
}