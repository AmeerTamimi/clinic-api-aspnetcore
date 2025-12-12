using ClinicAPI.Repositories;

namespace ClinicAPI.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepo _PateintRepo;

        public PatientService(IPatientRepo PatientRepo)
        {
            _PateintRepo = PatientRepo;
        }
    }
}
