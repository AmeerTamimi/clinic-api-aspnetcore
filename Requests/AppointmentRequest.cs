namespace ClinicAPI.Requests
{
    public class AppointmentRequest
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
