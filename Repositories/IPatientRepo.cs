using ClinicAPI.Models;

namespace ClinicAPI.Repositories
{
    public interface IPatientRepo
    {
        Task<Patient?> GetPatientByIdAsync(int patientId);
        Task<Patient> AddNewPatientAsync(Patient newPatient);
        Task<bool> UpdatePatientAsync(Patient patient);
        Task<bool> DeletePatientByIdAsync(int patientId);
        Task<int> GetPatientCountAsync();
        Task<List<Patient>> GetPatientPageAsync(int page , int pageSize);
        Task<List<Patient>> GetPatientByDoctorAsync(Doctor doctorId);
    }
}
