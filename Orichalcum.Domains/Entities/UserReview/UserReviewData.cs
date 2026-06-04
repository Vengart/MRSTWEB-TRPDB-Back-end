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

namespace Orichalcum.Domains.Entities.UserReview
{
    public class UserReviewData : SharedFields
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool IsLike { get; set; }

        [StringLength(300)]
        public string? Comment { get; set; }

        public int TargetUserId { get; set; }
        [JsonIgnore]
        public UserData? TargetUser { get; set; }

        public int AuthorUserId { get; set; }
        [JsonIgnore]
        public UserData? AuthorUser { get; set; }
    }
}