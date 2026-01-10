using ClinicAPI.Enums;

namespace ClinicAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public UserType Type { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }
        public List<string> Permissions { get; set; } = [];
        public List<string> Roles { get; set; } = [];
        public string? RefreshToken { get; set; }
        public RefreshTokenModel? RefreshTokenModel { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
