using ClinicAPI.Models;
using ClinicAPI.Repositories;
using ClinicAPI.Requests;
using ClinicAPI.Responses;

namespace ClinicAPI.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepo _patientRepo;
        private readonly IAppointmentRepo _appointmentRepo;

        public PatientService(IPatientRepo patientRepo, IAppointmentRepo appointmentRepo)
        {
            _patientRepo = patientRepo;
            _appointmentRepo = appointmentRepo;
        }

        public PatientResponse GetPatientById(int patientId)
        {
            var patient = _patientRepo.GetPatientById(patientId);

            if (patient is null)
                throw new ArgumentNullException("Patient Not Found");

            return PatientResponse.FromModel(patient, false);
        }

        public PatientResponse AddNewPatient(CreatePatientRequest NewPatient)
        {
            IsValidPatient(NewPatient);

            var patient = FromRequest(NewPatient);

            var createdPatient = _patientRepo.AddNewPatient(patient);

            return PatientResponse.FromModel(createdPatient, false);
        }

        public PatientResponse UpdatePatient(UpdatePatientRequest Patient, int PatientId)
        {
            IsValidPatient(Patient, PatientId);

            var patient = FromRequest(Patient, PatientId);

            var updatedPatient = _patientRepo.UpdatePatient(patient);

            return PatientResponse.FromModel(updatedPatient, true);
        }

        public PatientResponse DeletePatient(int patientId)
        {
            var patient = _patientRepo.GetPatientById(patientId);

            if (patient is null)
                throw new ArgumentNullException("Patient Does Not Exist");

            _patientRepo.DeletePatientById(patientId);

            return PatientResponse.FromModel(patient, true);
        }

        public List<AppointmentResponse> GetAppointmentByPatientId(int patientId)
        {
            var patient = _patientRepo.GetPatientById(patientId);

            if (patient is null)
                throw new ArgumentNullException("Patient Does Not Exist");

            var appointments = _appointmentRepo.GetAppointmentByPatientId(patientId);

            if (appointments is null)
                return [];

            patient.Appointments = appointments.ToList();

            return AppointmentResponse.FromModels(patient.Appointments)!.ToList();
        }

        public PagedResult<PatientResponse> GetPatientPage(int page, int pageSize, bool includeAppointments)
        {
            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, 100);

            int totalItems = _patientRepo.GetPatientCount();

            var patients = _patientRepo.GetPatientPage(page, pageSize);

            var patientsResponse = PatientResponse.FromModels(patients, includeAppointments);

            return PagedResult<PatientResponse>.GetPagedItems(patientsResponse, totalItems, page, pageSize);
        }

        private void IsValidPatient(PatientRequest patientRequest, int patientId = 0)
        {
            if (patientId > 0)
            {
                var patient = _patientRepo.GetPatientById(patientId);
                if (patient is null)
                    throw new ArgumentNullException("Patient Does Not Exist");
            }

            if (patientRequest is null)
                throw new ArgumentNullException("Invalid Patient Data");

            if (!patientRequest.FirstName.All(char.IsLetter) || !patientRequest.LastName.All(char.IsLetter))
                throw new ArgumentException("Name Must Contain Only Letters");

            if (patientRequest.Age < 0 || patientRequest.Age > 120)
                throw new ArgumentException("Invalid Age");
        }

        private Patient FromRequest(PatientRequest patientRequest, int id = 0)
        {
            var patient = new Patient
            {
                FirstName = patientRequest.FirstName,
                LastName = patientRequest.LastName,
                Age = patientRequest.Age,
                Symptoms = patientRequest.Symptoms,
                Medicine = patientRequest.Medicine,
                Diagnostic = patientRequest.Diagnostic,
                DoctorId = patientRequest.DoctorId
            };

            if (patientRequest is UpdatePatientRequest)
                patient.PatientId = id;

            return patient;
        }
    }
}
