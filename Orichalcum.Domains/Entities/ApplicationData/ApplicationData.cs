using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Orichalcum.Domains.Entities.Refs;
using Orichalcum.Domains.Entities.User;
using Orichalcum.Domains.Entities.GameSession;
using Orichalcum.Domains.Enums;

namespace Orichalcum.Domains.Entities.Application
{
    public class ApplicationData : SharedFields
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(500)]
        public string? Message { get; set; }    // сообщение игрока мастеру

        public ApplicationStatus Status { get; set; }  // Pending, Approved, Rejected

        // Связь с сессией
        public int GameSessionId { get; set; }
        [JsonIgnore]
        public GameSessionData? GameSession { get; set; }

        // Связь с игроком
        public int PlayerId { get; set; }
        [JsonIgnore]
        public UserData? Player { get; set; }


    }
}