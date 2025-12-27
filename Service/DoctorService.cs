using ClinicAPI.CustomExceptions;
using ClinicAPI.Models;
using ClinicAPI.Query;
using ClinicAPI.Repositories;
using ClinicAPI.Requests;
using ClinicAPI.Responses;

namespace ClinicAPI.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepo _doctorRepo;
        private readonly IPatientRepo _patientRepo;
        private readonly IAppointmentRepo _appointmentRepo;

        public DoctorService(IDoctorRepo doctorRepo, IPatientRepo patientRepo, IAppointmentRepo appointmentRepo)
        {
            _doctorRepo = doctorRepo;
            _patientRepo = patientRepo;
            _appointmentRepo = appointmentRepo;
        }

        public async Task<DoctorResponse> GetDoctorByIdAsync(int doctorId, DoctorQuery query)
        {
            var doctor = await _doctorRepo.GetDoctorByIdAsync(doctorId);

            if (doctor is null)
                throw new NotFoundException("Doctor Does Not Exist");

            return DoctorResponse.FromModel(doctor, query.IncludePatients, query.IncludeAppointments);
        }

        public async Task<DoctorResponse> AddNewDoctorAsync(CreateDoctorRequest newDoctor)
        {
            IsValidDoctor(newDoctor);

            Doctor doctor = FromCreateRequest(newDoctor);

            var created = await _doctorRepo.AddNewDoctorAsync(doctor);

            return DoctorResponse.FromModel(created, false, false);
        }

        public async Task UpdateDoctorAsync(UpdateDoctorRequest doctor, int doctorId)
        {
            await IsValidDoctorAsync(doctor, doctorId);

            Doctor doctorModel = FromUpdateRequest(doctor, doctorId);
        }

        public async Task<DoctorResponse> DeleteDoctorByIdAsync(int doctorId)
        {
            var doctor = await _doctorRepo.GetDoctorByIdAsync(doctorId);

            if (doctor is null)
                throw new NotFoundException("Doctor Does Not Exist");

            var succeded = await _doctorRepo.DeleteDoctorByIdAsync(doctorId);

            if (!succeded)
                throw new ServerException("Sorry, couldn't delete the Doctor");

            return DoctorResponse.FromModel(doctor, true, true);
        }

        public async Task<List<PatientResponse>> GetDoctorPatientsAsync(int doctorId, PatientQuery query)
        {
            var doctor = await _doctorRepo.GetDoctorByIdAsync(doctorId);

            if (doctor is null)
                throw new NotFoundException("Doctor Does Not Exist");

            var patients = await _patientRepo.GetPatientByDoctorAsync(doctor);

            if (patients is null)
                return [];

            return PatientResponse.FromModels(patients, query)!.ToList();
        }

        public async Task<List<AppointmentResponse>> GetDoctorAppointmentsAsync(int doctorId, AppointmentQuery query)
        {
            var doctor = await _doctorRepo.GetDoctorByIdAsync(doctorId);

            if (doctor is null)
                throw new NotFoundException("Doctor Does Not Exist");

            var appointments = await _appointmentRepo.GetAppointmentByDoctorIdAsync(doctorId);

            if (appointments is null)
                return [];

            return AppointmentResponse.FromModels(appointments, query)!.ToList();
        }

        public async Task<PagedResult<DoctorResponse>> GetDoctorPageAsync(DoctorQuery query)
        {
            int page = Math.Max(query.Page, 1);
            int pageSize = Math.Clamp(query.PageSize, 3, 100);

            var totalItems = await _doctorRepo.GetDoctorCountAsync();

            var doctorList = await _doctorRepo.GetDoctorPageAsync(page, pageSize);

            var doctorResponseList = DoctorResponse.FromModels(doctorList, query);

            return PagedResult<DoctorResponse>.GetPagedItems(doctorResponseList, totalItems, page, pageSize);
        }

        private void IsValidDoctor(CreateDoctorRequest doctorRequest)
        {
            if (doctorRequest is null)
                throw new ValidationException("Invalid Doctor Data");

            if (!doctorRequest.FirstName.All(char.IsLetter) || !doctorRequest.LastName.All(char.IsLetter))
                throw new ValidationException("Name Must Contain Only Letters");

            if (doctorRequest.Age < 0 || doctorRequest.Age > 120)
                throw new ValidationException("Invalid Age");

            if (doctorRequest.YearOfExperience < 0 || doctorRequest.YearOfExperience > 100)
                throw new ValidationException("Invalid Years Of Experience");

            if (!doctorRequest.Phone.All(char.IsDigit) || doctorRequest.Phone.Count() < 10 || doctorRequest.Phone.Count() > 12)
                throw new ValidationException("Invalid Phone Number");
        }

        private async Task IsValidDoctorAsync(UpdateDoctorRequest doctorRequest, int doctorId)
        {
            var doctor = await _doctorRepo.GetDoctorByIdAsync(doctorId);
            if (doctor is null)
                throw new NotFoundException("Doctor Does Not Exist");

            if (doctorRequest is null)
                throw new ValidationException("Invalid Doctor Data");

            if (!doctorRequest.FirstName.All(char.IsLetter) || !doctorRequest.LastName.All(char.IsLetter))
                throw new ValidationException("Name Must Contain Only Letters");

            if (doctorRequest.Age < 0 || doctorRequest.Age > 120)
                throw new ValidationException("Invalid Age");

            if (doctorRequest.YearOfExperience < 0 || doctorRequest.YearOfExperience > 100)
                throw new ValidationException("Invalid Years Of Experience");

            if (!doctorRequest.Phone.All(char.IsDigit) || doctorRequest.Phone.Count() < 10 || doctorRequest.Phone.Count() > 12)
                throw new ValidationException("Invalid Phone Number");
        }

        private Doctor FromCreateRequest(CreateDoctorRequest doctorRequest)
        {
            var doctor = new Doctor
            {
                FirstName = doctorRequest.FirstName,
                LastName = doctorRequest.LastName,
                Specialty = doctorRequest.Specialty,
                Phone = doctorRequest.Phone,
                Age = doctorRequest.Age,
                YearOfExperience = doctorRequest.YearOfExperience
            };

            return doctor;
        }

        private Doctor FromUpdateRequest(UpdateDoctorRequest doctorRequest, int doctorId)
        {
            var doctor = new Doctor
            {
                DoctorId = doctorId,
                FirstName = doctorRequest.FirstName,
                LastName = doctorRequest.LastName,
                Specialty = doctorRequest.Specialty,
                Phone = doctorRequest.Phone,
                Age = doctorRequest.Age,
                YearOfExperience = doctorRequest.YearOfExperience
            };

            return doctor;
        }
    }
}
