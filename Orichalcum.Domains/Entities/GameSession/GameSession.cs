using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Orichalcum.Domains.Entities.Refs;
using Orichalcum.Domains.Entities.User;
using Orichalcum.Domains.Enums;
using Orichalcum.Domains.Entities.Application;

namespace Orichalcum.Domains.Entities.GameSession
{
    public class GameSessionData : SharedFields
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(2000)]
        public string? Description { get; set; }

        [Required]
        public string System { get; set; }      // D&D 5e, Pathfinder, CoC и т.д.

        [Required]
        public string Setting { get; set; }     // сеттинг / мир

        public int MaxPlayers { get; set; }     // лимит игроков

        [StringLength(50)]
        public string? Duration { get; set; }

        [StringLength(100)]
        public string? Price { get; set; }

        public string? CoverImageUrl { get; set; }  // обложка сессии

        public SessionStatus Status { get; set; }   // Draft, Open, Closed, Archived

        public DateTime? ScheduledAt { get; set; }  // дата проведения

        // Геймастер — владелец сессии
        public int GameMasterId { get; set; }
        [JsonIgnore]
        public UserData? GameMaster { get; set; }

        // Заявки игроков
        [JsonIgnore]
        public List<ApplicationData>? Applications { get; set; }
    }
}