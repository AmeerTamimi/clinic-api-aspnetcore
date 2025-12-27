namespace ClinicAPI.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        public List<Appointment>? PatientAppointments { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

    }
}
