using ClinicAPI.Models;
using ClinicAPI.Query;
using ClinicAPI.Requests;
using ClinicAPI.Responses;

namespace ClinicAPI.Service
{
    public interface IPatientService
    {
        Task<PatientResponse> GetPatientByIdAsync(int patientId , bool includeAppointments = false, CancellationToken ct = default);
        Task<PatientResponse> AddNewPatientAsync(CreatePatientRequest newPatient, CancellationToken ct = default);
        Task UpdatePatientAsync(UpdatePatientRequest patient , int patientId, CancellationToken ct = default);
        Task<PatientResponse> DeletePatientAsync(int patientId, CancellationToken ct = default);
        Task<List<AppointmentResponse>> GetAppointmentByPatientIdAsync(int patientId , AppointmentQuery query, CancellationToken ct = default);
        Task<PagedResult<PatientResponse>> GetPatientPageAsync(PatientQuery query, CancellationToken ct = default);
    }
}
