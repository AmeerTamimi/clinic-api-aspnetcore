using ClinicAPI.Models;
using ClinicAPI.Requests;
using ClinicAPI.Responses;

namespace ClinicAPI.Service
{
    public interface IAppointmentService
    {
        AppointmentResponse AddNewAppointment(CreateAppointmentRequest NewAppointment);
        AppointmentResponse UpdateAppointment(UpdateAppointmentRequest Appointment , int AppointmentId);
    }
}
