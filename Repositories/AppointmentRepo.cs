using ClinicAPI.Models;
using ClinicAPI.Requests;

namespace ClinicAPI.Repositories
{
    public class AppointmentRepo : IAppointmentRepo
    {
        public Appointment AddNewAppointment(Appointment NewAppointment)
        {
            // We Add to DB here
            return new Appointment();
        }

        public Appointment GetAppointmentById(int AppointmentId)
        {
            return new Appointment();
        }

        public Appointment UpdateAppointment(Appointment Appointment , int AppointmentId)
        {
            return new Appointment();
        }
    }
}
