using Orichalcum.Domains.Enums;

namespace Orichalcum.Domains.Models.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Email { get; set; }
        public string? Bio { get; set; }
        public string? AvatarUrl { get; set; }
        public UserRole Role { get; set; }
    }
}