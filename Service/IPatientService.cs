using ClinicAPI.Models;
using ClinicAPI.Requests;
using ClinicAPI.Responses;

namespace ClinicAPI.Service
{
    public interface IPatientService
    {
        PatientResponse GetPatientById(int patientId);
        PatientResponse AddNewPatient(CreatePatientRequest NewPatient);
        PatientResponse UpdatePatient(UpdatePatientRequest Patient , int PatientId);
        PatientResponse DeletePatient(int patientId);
        List<AppointmentResponse> GetAppointmentByPatientId(int patientId);
        PagedResult<PatientResponse> GetPatientPage(int page, int pageSize,bool includeAppointments);
    }
}
