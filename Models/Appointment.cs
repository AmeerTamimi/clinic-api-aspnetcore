namespace ClinicAPI.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientUserId { get; set; }
        public int DoctorUserId { get; set; }
        public bool IsDone { get; set; }
        public string Symptoms { get; set; }
        public string? Medicine { get; set; }
        public string? Diagnostic { get; set; }
        public DateTimeOffset Date { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
