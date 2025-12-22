using ClinicAPI.Query;
using ClinicAPI.Requests;
using ClinicAPI.Responses;

namespace ClinicAPI.Service
{
    public interface IDoctorService
    {
        DoctorResponse GetDoctorById(int doctorId);
        DoctorResponse AddNewDoctor(CreateDoctorRequest newDoctor);
        DoctorResponse UpdateDoctor(UpdateDoctorRequest doctor, int doctorId);
        DoctorResponse DeleteDoctorById(int doctorId);
        List<PatientResponse> GetDoctorPatients(int doctorId, bool includeAppointments);
        List<AppointmentResponse> GetDoctorAppointments(int doctorId);
        PagedResult<DoctorResponse> GetDoctorPage(DoctorSearchRequest query);
    }
}
