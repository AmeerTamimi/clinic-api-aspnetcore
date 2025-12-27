using ClinicAPI.Models;
using ClinicAPI.Requests;
using ClinicAPI.Responses;
using ClinicAPI.CustomExceptions;
using ClinicAPI.Repositories;
using ClinicAPI.Query;

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

        public PatientResponse GetPatientById(int patientId , PatientQuery query)
        {
            var patient = _patientRepo.GetPatientById(patientId);

            if (patient is null)
                throw new NotFoundException("Patient Not Found");

            return PatientResponse.FromModel(patient, query.IncludeAppointments);
        }

        public PatientResponse AddNewPatient(CreatePatientRequest patientRequest)
        {
            IsValidPatient(patientRequest);

            var patient = FromCreateRequest(patientRequest);

            var createdPatient = _patientRepo.AddNewPatient(patient);

            return PatientResponse.FromModel(createdPatient, false);
        }

        public void UpdatePatient(UpdatePatientRequest patientRequest, int patientId)
        {
            IsValidPatient(patientRequest, patientId);

            var patient = FromUpdateRequest(patientRequest, patientId);

            var succed = _patientRepo.UpdatePatient(patient);

            if (!succed)
                throw new ServerException("Sorry, couldn't update the Patient");

        }

        public PatientResponse DeletePatient(int patientId)
        {
            var patient = _patientRepo.GetPatientById(patientId);

            if (patient is null)
                throw new NotFoundException($"Patient Does Not Exist With Id {patientId}");

            var succeded = _patientRepo.DeletePatientById(patientId);

            if (!succeded)
                throw new ServerException("Sorry, couldn't delete the Patient");

            return PatientResponse.FromModel(patient, true);
        }

        public List<AppointmentResponse> GetAppointmentByPatientId(int patientId , AppointmentQuery query)
        {
            var patient = _patientRepo.GetPatientById(patientId);

            if (patient is null)
                throw new ArgumentNullException("Patient Does Not Exist");

            var appointments = _appointmentRepo.GetAppointmentByPatientId(patientId);

            if (appointments is null)
                return [];

            patient.PatientAppointments = appointments.ToList();

            return AppointmentResponse.FromModels(patient.PatientAppointments , query)!.ToList();
        }

        public PagedResult<PatientResponse> GetPatientPage(PatientQuery query)
        {
            int page = query.Page;
            int pageSize = query.PageSize;

            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, 100);

            int totalItems = _patientRepo.GetPatientCount();

            var patients = _patientRepo.GetPatientPage(page, pageSize);

            var patientsResponse = PatientResponse.FromModels(patients, query);

            return PagedResult<PatientResponse>.GetPagedItems(patientsResponse, totalItems, page, pageSize);
        }

        private void IsValidPatient(CreatePatientRequest patientRequest)
        {

            if (patientRequest is null)
                throw new ValidationException("Invalid Patient Data");

            if (!patientRequest.FirstName.All(char.IsLetter) || !patientRequest.LastName.All(char.IsLetter))
                throw new ValidationException("Name Must Contain Only Letters");

            if (patientRequest.Age < 0 || patientRequest.Age > 130)
                throw new ValidationException("Age Must Be Between 0-130");
        }
        private void IsValidPatient(UpdatePatientRequest patientRequest , int patientId)
        {
            var patient = _patientRepo.GetPatientById(patientId);
            if (patient is null)
                throw new NotFoundException($"Patient with Id {patientId} Does Not Exist");

            if (patientRequest is null)
                throw new ValidationException("Invalid Patient Data");

            if (!patientRequest.FirstName.All(char.IsLetter) || !patientRequest.LastName.All(char.IsLetter))
                throw new ValidationException("Name Must Contain Only Letters");

            if (patientRequest.Age < 0 || patientRequest.Age > 120)
                throw new ValidationException("Age Must Be Between 0-120");
        }

        private Patient FromCreateRequest(CreatePatientRequest patientRequest)
        {
            var patient = new Patient
            {
                FirstName = patientRequest.FirstName,
                LastName = patientRequest.LastName,
                Age = patientRequest.Age,
                DoctorId = patientRequest.DoctorId
            };

            return patient;
        }
        private Patient FromUpdateRequest(UpdatePatientRequest patientRequest , int patientId)
        {
            var patient = new Patient
            {
                PatientId = patientId,
                FirstName = patientRequest.FirstName,
                LastName = patientRequest.LastName,
                Age = (int) patientRequest.Age,
                DoctorId = (int) patientRequest.DoctorId
            };
            return patient;
        }
    }
}
