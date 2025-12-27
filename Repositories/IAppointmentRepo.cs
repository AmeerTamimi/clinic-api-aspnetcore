using ClinicAPI.Models;

namespace ClinicAPI.Repositories
{
    public interface IAppointmentRepo
    {
        Task<Appointment?> GetAppointmentByIdAsync(int appointmentId);
        Task<Appointment> AddNewAppointmentAsync(Appointment newAppointment);
        Task UpdateAppointmentAsync(Appointment appointment, int appointmentId);
        Task<bool> DeleteAppointmentByIdAsync(int appointmentId);
        Task<int> GetAppointmentCountAsync();
        Task<List<Appointment>> GetAppointmentByDateAsync(DateTimeOffset from, DateTimeOffset to);
        Task<List<Appointment>> GetAppointmentPageAsync(int page, int pageSize);
        Task<List<Appointment>> GetAppointmentByPatientIdAsync(int patientId);
        Task<List<Appointment>> GetAppointmentByDoctorIdAsync(int doctorId);
        Task<bool> HasAppointmentConflictAsync(Appointment appointment);
    }
}
