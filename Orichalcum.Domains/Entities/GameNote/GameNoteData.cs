using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Orichalcum.Domains.Entities.Refs;
using Orichalcum.Domains.Entities.User;
using Orichalcum.Domains.Entities.GameCard;

namespace Orichalcum.Domains.Entities.GameNote
{
    public class GameNoteData : SharedFields
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Header { get; set; }

        [StringLength(5000)]
        public string? BodyText { get; set; }

        public bool IsVisibleToPlayers { get; set; }
        public int GameCardId { get; set; }
        [JsonIgnore]
        public GameCardData? GameCard { get; set; }
        public int AuthorId { get; set; }
        [JsonIgnore]
        public UserData? Author { get; set; }
    }
}