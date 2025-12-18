using ClinicAPI.Models;

namespace ClinicAPI.Responses
{
    public class AppointmentResponse
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public bool IsDone { get; set; }
        public DateTimeOffset Date { get; set; }

        private AppointmentResponse() { }

        public static AppointmentResponse FromModel(Appointment appointment)
        {
            var appointmentResponse = new AppointmentResponse();
            appointmentResponse.AppointmentId = appointment.AppointmentId;
            appointmentResponse.PatientId = appointment.PatientId;
            appointmentResponse.DoctorId = appointment.DoctorId;
            appointmentResponse.Date = appointment.Date;
            appointmentResponse.IsDone = appointment.IsDone;

            return appointmentResponse;
        }

        public static IEnumerable<AppointmentResponse>? FromModels(IEnumerable<Appointment>? appointments)
        {
            if (appointments == null)
                return null;
            return appointments.Select(a => FromModel(a));
        }
    }
}
