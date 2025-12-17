using ClinicAPI.Models;
using ClinicAPI.Requests;

namespace ClinicAPI.Repositories
{
    public interface IPatientRepo
    {
        Patient GetPatientById(int PatientId);
        Patient AddNewPatient(Patient NewPatient);
        Patient UpdatePatient(Patient Patient , int PatientId);
    }
}
