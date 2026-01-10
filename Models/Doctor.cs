using ClinicAPI.Enums;

namespace ClinicAPI.Models
{
    public class Doctor : User
    {
        public decimal Salary { get; set; }
        public DoctorSpecialty Specialty { get; set; }
        public int YearOfExperience { get; set; }
        public List<Patient>? DoctorPatients { get; set; }
        public List<Appointment>? DoctorAppointments { get; set; }
    }
}
