using ClinicAPI.Models;
using ClinicAPI.Responses;

namespace ClinicAPI.Repositories
{
    public interface IAppointmentRepo
    {
        Appointment GetAppointmentById(int AppointmentId);
        Appointment AddNewAppointment(Appointment NewAppointment);
        Appointment UpdateAppointment(Appointment Appointment , int AppointmentId);
    }
}
