namespace ClinicAPI.Requests
{
    public class UpdateAppointmentRequest : AppointmentRequest
    {
        public bool IsDone { get; set; }
    }
}
