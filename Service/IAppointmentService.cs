using ClinicAPI.Query;
using ClinicAPI.Requests;
using ClinicAPI.Responses;

namespace ClinicAPI.Service
{
    public interface IAppointmentService
    {
        Task<AppointmentResponse> GetAppointmentByIdAsync(int appointmentId, CancellationToken ct = default);
        Task<AppointmentResponse> AddNewAppointmentAsync(CreateAppointmentRequest newAppointment, CancellationToken ct = default);
        Task UpdateAppointmentAsync(UpdateAppointmentRequest appointment, int appointmentId, CancellationToken ct = default);
        Task<AppointmentResponse> DeleteAppointmentByIdAsync(int appointmentId, CancellationToken ct = default);
        Task<PagedResult<AppointmentResponse>> GetAppointmentPageAsync(AppointmentQuery query, CancellationToken ct = default);
    }
}
