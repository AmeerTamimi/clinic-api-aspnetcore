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
        public string Symptoms { get; set; } = default!;
        public string? Diagnostic { get; set; }
        public string? Medicine { get; set; }

        private AppointmentResponse() { }

        public static AppointmentResponse FromModel(Appointment appointment)
        {
            var appointmentResponse = new AppointmentResponse
            {
                AppointmentId = appointment.AppointmentId,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                Date = appointment.Date,
                IsDone = appointment.IsDone,
                Symptoms = appointment.Symptoms,
                Diagnostic = appointment.Diagnostic,
                Medicine = appointment.Medicine
            };

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
