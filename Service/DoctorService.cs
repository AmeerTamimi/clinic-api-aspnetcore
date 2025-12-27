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

        public DoctorResponse GetDoctorById(int doctorId)
        {
            var doctor = _doctorRepo.GetDoctorById(doctorId);

            if (doctor is null)
                throw new NotFoundException("Doctor Does Not Exist");

            return DoctorResponse.FromModel(doctor, false, false);
        }

        public DoctorResponse AddNewDoctor(CreateDoctorRequest newDoctor)
        {
            IsValidDoctor(newDoctor);

            Doctor doctor = FromCreateRequest(newDoctor);

            _doctorRepo.AddNewDoctor(doctor);

            return DoctorResponse.FromModel(doctor, false, false);
        }

        public void UpdateDoctor(UpdateDoctorRequest doctor, int doctorId)
        {
            IsValidDoctor(doctor, doctorId);

            Doctor doctorModel = FromUpdateRequest(doctor, doctorId);

            _doctorRepo.UpdateDoctor(doctorModel, doctorId);
        }

        public DoctorResponse DeleteDoctorById(int doctorId)
        {
            var doctor = _doctorRepo.GetDoctorById(doctorId);

            if (doctor is null)
                throw new NotFoundException("Doctor Does Not Exist");

            _doctorRepo.DeleteDoctorById(doctorId);

            return DoctorResponse.FromModel(doctor, true, true);
        }

        public List<PatientResponse> GetDoctorPatients(int doctorId, bool includeAppointments)
        {
            var doctor = _doctorRepo.GetDoctorById(doctorId);

            if (doctor is null)
                throw new NotFoundException("Doctor Does Not Exist");

            var patients = _patientRepo.GetPatientByDoctor(doctor);

            if (patients is null)
                return [];

            return PatientResponse.FromModels(patients, includeAppointments)!.ToList();
        }

        public List<AppointmentResponse> GetDoctorAppointments(int doctorId)
        {
            var doctor = _doctorRepo.GetDoctorById(doctorId);

            if (doctor is null)
                throw new NotFoundException("Doctor Does Not Exist");

            var appointments = _appointmentRepo.GetAppointmentByDoctorId(doctorId);

            if (appointments is null)
                return [];

            return AppointmentResponse.FromModels(appointments)!.ToList();
        }

        public PagedResult<DoctorResponse> GetDoctorPage(DoctorSearchRequest query)
        {
            int page = Math.Max(query.Page, 1);
            int pageSize = Math.Clamp(query.PageSize, 3, 100);

            var doctorList = _doctorRepo.GetDoctorPage(page, pageSize);

            var doctorResponseList = DoctorResponse.FromModels(doctorList, true, true);

            return PagedResult<DoctorResponse>.GetPagedItems(doctorResponseList, doctorList.Count(), page, pageSize);
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

        private void IsValidDoctor(UpdateDoctorRequest doctorRequest, int doctorId)
        {
            var doctor = _doctorRepo.GetDoctorById(doctorId);
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
