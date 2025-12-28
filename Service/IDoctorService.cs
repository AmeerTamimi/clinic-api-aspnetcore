using ClinicAPI.Query;
using ClinicAPI.Requests;
using ClinicAPI.Responses;

namespace ClinicAPI.Service
{
    public interface IDoctorService
    {
        Task<DoctorResponse> GetDoctorByIdAsync(int doctorId, DoctorQuery query, CancellationToken ct = default);
        Task<DoctorResponse> AddNewDoctorAsync(CreateDoctorRequest newDoctor, CancellationToken ct = default);
        Task UpdateDoctorAsync(UpdateDoctorRequest doctor, int doctorId, CancellationToken ct = default);
        Task<DoctorResponse> DeleteDoctorByIdAsync(int doctorId, CancellationToken ct = default);
        Task<List<PatientResponse>> GetDoctorPatientsAsync(int doctorId, PatientQuery query, CancellationToken ct = default);
        Task<List<AppointmentResponse>> GetDoctorAppointmentsAsync(int doctorId, AppointmentQuery query, CancellationToken ct = default);
        Task<PagedResult<DoctorResponse>> GetDoctorPageAsync(DoctorQuery query, CancellationToken ct = default);
    }
}
