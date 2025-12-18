using ClinicAPI.Models;
using ClinicAPI.Requests;
using ClinicAPI.Responses;

namespace ClinicAPI.Service
{
    public interface IPatientService
    {

        PatientResponse AddNewPatient(CreatePatientRequest NewPatient);
        PatientResponse UpdatePatient(UpdatePatientRequest Patient , int PatientId);
        PatientResponse DeletePatientById(int patientId);
        PagedResult<PatientResponse> GetPatientPage(int page, int pageSize);
        List<PatientResponse> GetPatientByDoctorId(int doctorId);
    }
}
