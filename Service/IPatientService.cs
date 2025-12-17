using ClinicAPI.Requests;
using ClinicAPI.Responses;

namespace ClinicAPI.Service
{
    public interface IPatientService
    {
        PatientResponse AddNewPatient(CreatePatientRequest NewPatient);
        PatientResponse UpdatePatient(UpdatePatientRequest Patient , int PatientId);
    }
}
