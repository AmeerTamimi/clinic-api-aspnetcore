using ClinicAPI.Models;
using ClinicAPI.Requests;
using ClinicAPI.Responses;
using ClinicAPI.CustomExceptions;
using ClinicAPI.Repositories;
using ClinicAPI.Query;
using Microsoft.AspNetCore.DataProtection;

namespace ClinicAPI.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepo _patientRepo;
        private readonly IAppointmentRepo _appointmentRepo;
        private readonly IDataProtector _protector;

        public PatientService(IPatientRepo patientRepo,
                              IAppointmentRepo appointmentRepo ,
                              IDataProtectionProvider protectionProvider)
        {
            _patientRepo = patientRepo;
            _appointmentRepo = appointmentRepo;
            _protector = protectionProvider.CreateProtector("Patient.Protection");
        }

        public async Task<PatientResponse> GetPatientByIdAsync(int patientId , bool includeAppointments = false, CancellationToken ct = default)
        {
            var patient = await _patientRepo.GetPatientByIdAsync(patientId, ct);

            if (patient is null)
                throw new NotFoundException("Patient Not Found");

            return PatientResponse.FromModel(patient, includeAppointments , _protector);
        }

        public async Task<PatientResponse> AddNewPatientAsync(CreatePatientRequest patientRequest, CancellationToken ct = default)
        {
            IsValidPatient(patientRequest);

            var patient = FromCreateRequest(patientRequest);

            var createdPatient = await _patientRepo.AddNewPatientAsync(patient, ct);

            return PatientResponse.FromModel(createdPatient, false , _protector);
        }

        public async Task UpdatePatientAsync(UpdatePatientRequest patientRequest, int patientId, CancellationToken ct = default)
        {
            IsValidPatient(patientRequest, patientId);

            var patient = FromUpdateRequest(patientRequest, patientId);

            var succed = await _patientRepo.UpdatePatientAsync(patient, ct);

            if (!succed)
                throw new ServerException("Sorry, couldn't update the Patient");

        }

        public async Task<PatientResponse> DeletePatientAsync(int patientId, CancellationToken ct = default)
        {
            var patient = await  _patientRepo.GetPatientByIdAsync(patientId, ct);

            if (patient is null)
                throw new NotFoundException($"Patient Does Not Exist With Id {patientId}");

            var succeded = await _patientRepo.DeletePatientByIdAsync(patientId, ct);

            if (!succeded)
                throw new ServerException("Sorry, couldn't delete the Patient");

            return PatientResponse.FromModel(patient, true , _protector);
        }

        public async Task<List<AppointmentResponse>> GetAppointmentByPatientIdAsync(int patientId , AppointmentQuery query, CancellationToken ct = default)
        {
            var patient = await _patientRepo.GetPatientByIdAsync(patientId , ct);

            if (patient is null)
                throw new ArgumentNullException("Patient Does Not Exist");

            var appointments = await _appointmentRepo.GetAppointmentByPatientIdAsync(patientId , ct);

            if (appointments is null)
                return [];

            patient.PatientAppointments = appointments.ToList();

            return AppointmentResponse.FromModels(patient.PatientAppointments , query)!.ToList();
        }

        public async Task<PagedResult<PatientResponse>> GetPatientPageAsync(PatientQuery query , CancellationToken ct = default)
        {
            int page = query.Page;
            int pageSize = query.PageSize;

            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, 100);

            int totalItems = await _patientRepo.GetPatientCountAsync(ct);

            var patients = await _patientRepo.GetPatientPageAsync(page, pageSize , ct);

            var patientsResponse = PatientResponse.FromModels(patients, query , _protector);

            return PagedResult<PatientResponse>.GetPagedItems(patientsResponse!, totalItems, page, pageSize);
        }

        private void IsValidPatient(CreatePatientRequest patientRequest)
        {

            if (patientRequest is null)
                throw new ValidationException("Invalid Patient Data");

            if (!patientRequest.FirstName!.All(char.IsLetter) || !patientRequest.LastName!.All(char.IsLetter))
                throw new ValidationException("Name Must Contain Only Letters");

            if (patientRequest.Age < 0 || patientRequest.Age > 130)
                throw new ValidationException("Age Must Be Between 0-130");
        }
        private async void IsValidPatient(UpdatePatientRequest patientRequest , int patientId)
        {
            var patient = await _patientRepo.GetPatientByIdAsync(patientId);
            if (patient is null)
                throw new NotFoundException($"Patient with Id {patientId} Does Not Exist");

            if (patientRequest is null)
                throw new ValidationException("Invalid Patient Data");

            if (!patientRequest.FirstName!.All(char.IsLetter) || !patientRequest.LastName!.All(char.IsLetter))
                throw new ValidationException("Name Must Contain Only Letters");

            if (patientRequest.Age < 0 || patientRequest.Age > 120)
                throw new ValidationException("Age Must Be Between 0-120");
        }

        private Patient FromCreateRequest(CreatePatientRequest patientRequest)
        {
            var patient = new Patient
            {
                FirstName = _protector.Protect(patientRequest.FirstName!),
                LastName = _protector.Protect(patientRequest.LastName!),
                Email = _protector.Protect(patientRequest.Email!),
                Phone = _protector.Protect(patientRequest.Phone!),
                PasswordHash = _protector.Protect(patientRequest.Password!),
                Age = patientRequest.Age,
                DoctorId = patientRequest.DoctorId,
                RiskLevel = patientRequest.RiskLevel,
                BloodType = patientRequest.BloodType,
                Allergies = _protector.Protect(patientRequest.Allergies!),
                Note = _protector.Protect(patientRequest.Note!)
            };

            return patient;
        }
        private Patient FromUpdateRequest(UpdatePatientRequest patientRequest , int patientId)
        {
            var patient = new Patient
            {
                UserId = patientId,
                FirstName = patientRequest.FirstName!,
                LastName = patientRequest.LastName!,
                Age = (int) patientRequest.Age,
                DoctorId = (int) patientRequest.DoctorId
            };
            return patient;
        }
    }
}
