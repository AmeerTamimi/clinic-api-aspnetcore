namespace ClinicAPI.Configurations
{
    public class JwtSettings
    {
        public const string JwtSettingsName = "JwtSettings";

        public string? Audience { get; set; }
        public string? Issuer { get; set; }
        public int TokenExpirationInMinutes { get; set; }
        public string? SecretKey { get; set; }

    }
}
