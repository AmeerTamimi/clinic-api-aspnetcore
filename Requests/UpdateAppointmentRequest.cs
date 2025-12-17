namespace ClinicAPI.Requests
{
    public class UpdateAppointmentRequest
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public bool IsDone { get; set; }
        public DateOnly Date { get; set; }
    }
}
