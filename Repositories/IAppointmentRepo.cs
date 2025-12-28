using ClinicAPI.Models;

namespace ClinicAPI.Repositories
{
    public interface IAppointmentRepo
    {
        Task<Appointment?> GetAppointmentByIdAsync(int appointmentId, CancellationToken ct = default);
        Task<Appointment> AddNewAppointmentAsync(Appointment newAppointment, CancellationToken ct = default);
        Task UpdateAppointmentAsync(Appointment appointment, int appointmentId, CancellationToken ct = default);
        Task<bool> DeleteAppointmentByIdAsync(int appointmentId, CancellationToken ct = default);
        Task<int> GetAppointmentCountAsync(CancellationToken ct = default);
        Task<List<Appointment>> GetAppointmentByDateAsync(DateTimeOffset from, DateTimeOffset to, CancellationToken ct = default);
        Task<List<Appointment>> GetAppointmentPageAsync(int page, int pageSize, CancellationToken ct = default);
        Task<List<Appointment>> GetAppointmentByPatientIdAsync(int patientId, CancellationToken ct = default);
        Task<List<Appointment>> GetAppointmentByDoctorIdAsync(int doctorId, CancellationToken ct = default);
        Task<bool> HasAppointmentConflictAsync(Appointment appointment, CancellationToken ct = default);
    }
}
