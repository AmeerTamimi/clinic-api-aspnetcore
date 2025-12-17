using ClinicAPI.Models;
using ClinicAPI.Requests;

namespace ClinicAPI.Repositories
{
    public class PatientRepo : IPatientRepo
    {
        public Patient AddNewPatient(Patient NewPatient)
        {
            // We Add to DB here
            return NewPatient;
        }

        public Patient GetPatientById(int PatientId)
        {
            return new Patient();
        }

        public Patient UpdatePatient(Patient Patient , int PatientId)
        {
            return Patient;
        }
    }
}
