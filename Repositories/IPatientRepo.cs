using ClinicAPI.Models;

namespace ClinicAPI.Repositories
{
    public interface IPatientRepo
    {
        Patient? GetPatientById(int patientId);
        Patient AddNewPatient(Patient newPatient);
        Patient UpdatePatient(Patient patient);
        Patient DeletePatient(Patient patient);
        int GetPatientCount();
        List<Patient> GetPatientPage(int page , int pageSize);
        List<Patient> GetPatientByDoctor(Doctor doctorId);
    }
}
