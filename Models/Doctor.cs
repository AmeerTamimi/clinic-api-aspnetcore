using ClinicAPI.Enums;

namespace ClinicAPI.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }
        public List<string> Permissions { get; set; } = [];
        public List<string> Roles { get; set; } = [];
        public DoctorSpecialty Specialty { get; set; }
        public int YearOfExperience { get; set; }
        public List<Patient>? DoctorPatients { get; set; }
        public List<Appointment>? DoctorAppointments { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
