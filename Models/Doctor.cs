using ClinicAPI.Enums;

namespace ClinicAPI.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DoctorSpecialty Specialty { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public int YearOfExperience { get; set; }
        public List<Patient>? Patients { get; set; }
        public List<Appointment>? Appointments { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
