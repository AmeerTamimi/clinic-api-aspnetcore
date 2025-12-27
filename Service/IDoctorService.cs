using ClinicAPI.Query;
using ClinicAPI.Requests;
using ClinicAPI.Responses;

namespace ClinicAPI.Service
{
    public interface IDoctorService
    {
        DoctorResponse GetDoctorById(int doctorId , DoctorQuery query);
        DoctorResponse AddNewDoctor(CreateDoctorRequest newDoctor);
        void UpdateDoctor(UpdateDoctorRequest doctor, int doctorId);
        DoctorResponse DeleteDoctorById(int doctorId);
        List<PatientResponse> GetDoctorPatients(int doctorId, PatientQuery query);
        List<AppointmentResponse> GetDoctorAppointments(int doctorId, AppointmentQuery query);
        PagedResult<DoctorResponse> GetDoctorPage(DoctorQuery query);
    }
}
