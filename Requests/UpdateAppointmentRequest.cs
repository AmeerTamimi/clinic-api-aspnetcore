namespace ClinicAPI.Requests
{
    public class UpdateAppointmentRequest
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string Symptoms { get; set; }
        public string? Medicine { get; set; }
        public string? Diagnostic { get; set; }
        public DateTimeOffset Date { get; set; }
        public bool IsDone { get; set; }
    }
}
