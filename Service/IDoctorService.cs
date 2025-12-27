using ClinicAPI.Query;
using ClinicAPI.Requests;
using ClinicAPI.Responses;

namespace ClinicAPI.Service
{
    public interface IDoctorService
    {
        Task<DoctorResponse> GetDoctorByIdAsync(int doctorId, DoctorQuery query);
        Task<DoctorResponse> AddNewDoctorAsync(CreateDoctorRequest newDoctor);
        Task UpdateDoctorAsync(UpdateDoctorRequest doctor, int doctorId);
        Task<DoctorResponse> DeleteDoctorByIdAsync(int doctorId);
        Task<List<PatientResponse>> GetDoctorPatientsAsync(int doctorId, PatientQuery query);
        Task<List<AppointmentResponse>> GetDoctorAppointmentsAsync(int doctorId, AppointmentQuery query);
        Task<PagedResult<DoctorResponse>> GetDoctorPageAsync(DoctorQuery query);
    }
}
