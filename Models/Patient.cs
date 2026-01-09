namespace ClinicAPI.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }
        public List<string> Permissions { get; set; } = [];
        public List<string> Roles { get; set; } = [];
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        public List<Appointment>? PatientAppointments { get; set; }
        public string? RefreshTokenHash { get; set; }
        public RefreshTokenModel? RefreshToken { get; set; }


        public bool IsDeleted { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
