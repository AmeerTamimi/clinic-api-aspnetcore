using ClinicAPI.Models;

namespace ClinicAPI.Repositories
{
    public interface IDoctorRepo
    {
        Task<Doctor?> GetDoctorByIdAsync(int doctorId, CancellationToken ct = default);
        Task<Doctor> AddNewDoctorAsync(Doctor newDoctor, CancellationToken ct = default);
        Task UpdateDoctorAsync(Doctor doctor, int doctorId, CancellationToken ct = default);
        Task<bool> DeleteDoctorByIdAsync(int doctorId, CancellationToken ct = default);
        Task<int> GetDoctorCountAsync(CancellationToken ct = default);
        Task<List<Doctor>> GetDoctorPageAsync(int page, int pageSize, CancellationToken ct = default);
        Task<List<Appointment>> GetAppointmentAsync(int doctorId, CancellationToken ct = default);
    }
}