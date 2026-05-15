//using Orichalcum.Domains.Entities.GameCard;
using Orichalcum.Domains.Entities.Application;
using Orichalcum.Domains.Entities.GameCard;
using Orichalcum.Domains.Entities.GameSession;
using Orichalcum.Domains.Entities.Refs;
using Orichalcum.Domains.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Orichalcum.Domains.Entities.User
{
    public class UserData : SharedFields
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string? UserName { get; set; }

        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }
        public string Password { get; set; }

        [Required]
        [StringLength(60)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(10000)]
        public string? Bio { get; set; }           // "о себе" — из твоей диаграммы

        public string? AvatarUrl { get; set; }     // аватар игрока

        public UserRole Role { get; set; }         // Player / GameMaster / Admin

        public bool IsActive { get; set; }

        // Навигационные свойства
        [JsonIgnore]
        public List<GameSessionData>? GameSessions { get; set; }  // созданные сессии (если GM)

        [JsonIgnore]
        public List<ApplicationData>? Applications { get; set; }  // заявки на чужие сессии

        [JsonIgnore]
        public List<GameCardData>? GameCards { get; set; }
    }
}