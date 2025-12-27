using ClinicAPI.Query;
using ClinicAPI.Requests;
using ClinicAPI.Responses;

namespace ClinicAPI.Service
{
    public interface IAppointmentService
    {
        Task<AppointmentResponse> GetAppointmentByIdAsync(int appointmentId);
        Task<AppointmentResponse> AddNewAppointmentAsync(CreateAppointmentRequest newAppointment);
        Task UpdateAppointmentAsync(UpdateAppointmentRequest appointment, int appointmentId);
        Task<AppointmentResponse> DeleteAppointmentByIdAsync(int appointmentId);
        Task<PagedResult<AppointmentResponse>> GetAppointmentPageAsync(AppointmentQuery query);
    }
}
