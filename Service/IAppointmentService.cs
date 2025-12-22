using ClinicAPI.Models;
using ClinicAPI.Query;
using ClinicAPI.Requests;
using ClinicAPI.Responses;

namespace ClinicAPI.Service
{
    public interface IAppointmentService
    {
        AppointmentResponse GetAppointmentById(int appointmentId);
        AppointmentResponse AddNewAppointment(CreateAppointmentRequest newAppointment);
        AppointmentResponse UpdateAppointment(UpdateAppointmentRequest appointment , int appointmentId);
        AppointmentResponse DeleteAppointmentById(int appointmentId);
        PagedResult<AppointmentResponse> GetAppointmentPage(int page , int pageSize);
    }
}
