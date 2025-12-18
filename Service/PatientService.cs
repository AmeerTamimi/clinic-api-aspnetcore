using ClinicAPI.Requests;
using ClinicAPI.Repositories;
using ClinicAPI.Responses;
using ClinicAPI.Models;

namespace ClinicAPI.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepo _patientRepo;
        private readonly IDoctorRepo _doctorRepo;

        public PatientService(IPatientRepo PatientRepo , IDoctorRepo DoctorRepo)
        {
            _patientRepo = PatientRepo;
            _doctorRepo = DoctorRepo;
        }

        public PatientResponse AddNewPatient(CreatePatientRequest newPatient)
        {
            IsValidPatient(newPatient);

            var patient = FromRequest(newPatient);

            var createdPatient = _patientRepo.AddNewPatient(patient);

            return PatientResponse.FromModel(createdPatient);
        }

        public PatientResponse UpdatePatient(UpdatePatientRequest Patient, int PatientId)
        {
            IsValidPatient(Patient);

            var patient = FromRequest(Patient , PatientId);

            var updatedPatient = _patientRepo.UpdatePatient(patient);

            return PatientResponse.FromModel(updatedPatient);
        }

        public PatientResponse DeletePatientById(int patientId)
        {
            var patient = _patientRepo.GetPatientById(patientId);

            if (patient == null) 
                throw new ArgumentNullException("Patient Does Not Exist");

            _patientRepo.DeletePatient(patient);

            return PatientResponse.FromModel(patient);
        }

        public List<PatientResponse> GetPatientByDoctorId(int doctorId)
        {
            var doctor = _doctorRepo.GetDoctorById(doctorId);

            if (doctor is null) throw new ArgumentNullException("Doctor Does Not Exist !");

            var patients = _patientRepo.GetPatientByDoctor(doctor);

            if (patients is null) return [];

            return PatientResponse.FromModels(patients).ToList();
        }

        public PagedResult<PatientResponse> GetPatientPage(int page, int pageSize)
        {
            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, 100); // Gives 1 at least - 100 at most

            int totalItems = _patientRepo.GetPatientCount();

            var patients = _patientRepo.GetPatientPage(page, pageSize);

            var patientsResponse = PatientResponse.FromModels(patients);

            var currentPage = PagedResult<PatientResponse>.GetPagedItems(patientsResponse, totalItems, page, pageSize);

            return currentPage;
        }

        private void IsValidPatient(PatientRequest patientRequest)
        {
            if (patientRequest == null) 
                throw new ArgumentNullException("Invalid Patient Data (null)");

            if (!patientRequest.FirstName.All(char.IsLetter) || !patientRequest.LastName.All(char.IsLetter)) 
                throw new ArgumentException("Name Must Contain Only Letters");

            if (patientRequest.Age < 0 || patientRequest.Age > 120) 
                throw new ArgumentException("Invalid Age");

            var doctor = _doctorRepo.GetDoctorById(patientRequest.DoctorId);

            if(doctor == null) 
                throw new ArgumentNullException("Invalid Doctor (null)");
        }
        private Patient FromRequest(PatientRequest patientRequest , int id = 0)
        {
            var patient = new Patient
            {
                FirstName = patientRequest.FirstName,
                LastName = patientRequest.LastName,
                Age = patientRequest.Age,
                Symptoms = patientRequest.Symptoms,
                Medicine = patientRequest.Medicine,
                Diagnostic = patientRequest.Diagnostic,
                DoctorId = patientRequest.DoctorId,
            };

            if(patientRequest is UpdatePatientRequest u)
            {
                patient.PatientId = id;
                if (u.Appointments is not null) patient.Appointments = u.Appointments;
            }
            return patient;
        }
    }
}
