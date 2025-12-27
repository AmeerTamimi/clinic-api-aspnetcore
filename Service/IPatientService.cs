using ClinicAPI.Models;
using ClinicAPI.Query;
using ClinicAPI.Requests;
using ClinicAPI.Responses;

namespace ClinicAPI.Service
{
    public interface IPatientService
    {
        PatientResponse GetPatientById(int patientId , PatientQuery query);
        PatientResponse AddNewPatient(CreatePatientRequest newPatient);
        void UpdatePatient(UpdatePatientRequest patient , int patientId);
        PatientResponse DeletePatient(int patientId);
        List<AppointmentResponse> GetAppointmentByPatientId(int patientId , AppointmentQuery query);
        PagedResult<PatientResponse> GetPatientPage(PatientQuery query);
    }
}
