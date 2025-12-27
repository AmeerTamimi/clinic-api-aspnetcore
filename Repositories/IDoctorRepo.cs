using ClinicAPI.Models;

namespace ClinicAPI.Repositories
{
    public interface IDoctorRepo
    {
        Task<Doctor?> GetDoctorByIdAsync(int doctorId);
        Task<Doctor> AddNewDoctorAsync(Doctor newDoctor);
        Task UpdateDoctorAsync(Doctor doctor, int doctorId);
        Task<bool> DeleteDoctorByIdAsync(int doctorId);
        Task<int> GetDoctorCountAsync();
        Task<List<Doctor>> GetDoctorPageAsync(int page, int pageSize);
        Task<List<Appointment>> GetAppointmentAsync(int doctorId);
    }
}
