using ClinicAPI.Models;

namespace ClinicAPI.Repositories
{
    public interface IPatientRepo
    {
        Task<Patient?> GetPatientByIdAsync(int patientId, CancellationToken ct = default);
        Task<Patient> AddNewPatientAsync(Patient newPatient, CancellationToken ct = default);
        Task<bool> UpdatePatientAsync(Patient patient, CancellationToken ct = default);
        Task<bool> DeletePatientByIdAsync(int patientId, CancellationToken ct = default);
        Task<int> GetPatientCountAsync(CancellationToken ct = default);
        Task<List<Patient>> GetPatientPageAsync(int page , int pageSize, CancellationToken ct = default);
        Task<List<Patient>> GetPatientByDoctorAsync(Doctor doctorId, CancellationToken ct = default);
    }
}
