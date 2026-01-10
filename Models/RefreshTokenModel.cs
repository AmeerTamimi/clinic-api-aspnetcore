namespace ClinicAPI.Models
{
    public class RefreshTokenModel
    {
        public int RefreshTokenId { get; set; }
        public string RefreshTokenHash { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public DateTime Expires { get; set; }
    }
}
