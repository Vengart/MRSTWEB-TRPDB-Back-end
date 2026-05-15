using Microsoft.VisualBasic;
using Orichalcum.Domains.Entities.GameNote;
using Orichalcum.Domains.Entities.Refs;
using Orichalcum.Domains.Entities.User;
using Orichalcum.Domains.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Orichalcum.Domains.Entities.GameCard
{
    public class GameCardData : SharedFields
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Title { get; set; }

        public string? Content { get; set; }       // основной текст карточки

        public string? CoverImageUrl { get; set; } // обложка карточки

        public CardType Type { get; set; }         // Character, Location, Item, Lore и т.д.

        public bool IsPublic { get; set; }         // видна ли игрокам

        // Владелец карточки
        public int OwnerId { get; set; }
        [JsonIgnore]
        public UserData? Owner { get; set; }

        // Заметки к карточке
        [JsonIgnore]
        public List<GameNoteData>? Notes { get; set; }
    }
}