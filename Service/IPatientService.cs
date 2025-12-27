using ClinicAPI.Models;
using ClinicAPI.Query;
using ClinicAPI.Requests;
using ClinicAPI.Responses;

namespace ClinicAPI.Service
{
    public interface IPatientService
    {
        Task<PatientResponse> GetPatientByIdAsync(int patientId , PatientQuery query);
        Task<PatientResponse> AddNewPatientAsync(CreatePatientRequest newPatient);
        Task UpdatePatientAsync(UpdatePatientRequest patient , int patientId);
        Task<PatientResponse> DeletePatientAsync(int patientId);
        Task<List<AppointmentResponse>> GetAppointmentByPatientIdAsync(int patientId , AppointmentQuery query);
        Task<PagedResult<PatientResponse>> GetPatientPageAsync(PatientQuery query);
    }
}
