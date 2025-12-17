using ClinicAPI.Requests;
using ClinicAPI.Repositories;
using ClinicAPI.Responses;
using ClinicAPI.Models;

namespace ClinicAPI.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepo _PateintRepo;

        public PatientService(IPatientRepo PatientRepo)
        {
            _PateintRepo = PatientRepo;
        }
        public PatientResponse AddNewPatient(CreatePatientRequest NewPatient)
        {
            // After Validating The Patient (This is the core Service layer purpose)
            // We gonna add it to the DB using the infrastructure layer (we dont deal with DB stuff here)
            Patient patient = CreatePatient(NewPatient);

            _PateintRepo.AddNewPatient(patient);

            PatientResponse patientResponse = CreatePatientResponse(patient);
            return patientResponse;
        }

        public PatientResponse UpdatePatient(UpdatePatientRequest UpdatedPatient , int PatientId)
        {
            if(PatientId <= 0) throw new ArgumentOutOfRangeException("Patient Id Must be > 0");
            if (UpdatedPatient == null) throw new ArgumentNullException(nameof(UpdatedPatient));

            Patient CurrentPatient = _PateintRepo.GetPatientById(PatientId);

            if (CurrentPatient == null) throw new KeyNotFoundException("Patient Doesnt Exists !");

            // After More Business Validation...

            CurrentPatient.PatientId = PatientId;
            CurrentPatient.FirstName = UpdatedPatient.FirstName;
            CurrentPatient.LastName = UpdatedPatient.LastName;
            CurrentPatient.Age = UpdatedPatient.Age;
            CurrentPatient.Symptoms = UpdatedPatient.Symptoms;
            CurrentPatient.Medicine = UpdatedPatient.Medicine;
            CurrentPatient.Diagnostic = UpdatedPatient.Diagnostic;
            CurrentPatient.DoctorId = UpdatedPatient.DoctorId;

            _PateintRepo.UpdatePatient(CurrentPatient, PatientId);

            PatientResponse patientResponse = CreatePatientResponse(CurrentPatient);
            return patientResponse;
        }

        private Patient CreatePatient(CreatePatientRequest patient)
        {
            return new Patient
            {
                PatientId = 1,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Age = patient.Age,
                Symptoms = patient.Symptoms,
                Medicine = patient.Medicine,
                Diagnostic = patient.Diagnostic,
                DoctorId = patient.DoctorId,
                Appointments = null,
                CreatedAt = DateTimeOffset.Now,
                IsDeleted = false
            };
        }
        private PatientResponse CreatePatientResponse(Patient patient)
        {
            return new PatientResponse
            {
                PatientId = patient.PatientId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Age = patient.Age,
                Symptoms = patient.Symptoms,
                Medicine = patient.Medicine,
                Diagnostic = patient.Diagnostic,
                DoctorId = patient.DoctorId
            };
        }
    }
}
