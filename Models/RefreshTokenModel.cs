namespace ClinicAPI.Models
{
    public class RefreshTokenModel
    {
        public string? RefreshTokenHash { get; set; }
        public int UserId { get; set; }
        public Patient? Patient { get; set; }
        public DateTime Expires { get; set; }
    }
}
