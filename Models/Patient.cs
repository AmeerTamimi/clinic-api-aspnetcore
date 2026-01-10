using ClinicAPI.Enums;

namespace ClinicAPI.Models
{
    public class Patient : User
    {
        public RiskLevel? RiskLevel { get; set; }
        public BloodType? BloodType { get; set; }
        public string? Allergies { get; set; }
        public string? Note { get; set; }
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        public List<Appointment>? PatientAppointments { get; set; }
    }
}
