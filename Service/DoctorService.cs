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
                throw new ArgumentNullException("Doctor Does Not Exist");

            return DoctorResponse.FromModel(doctor, false, false);
        }

        public DoctorResponse AddNewDoctor(CreateDoctorRequest newDoctor)
        {
            IsValidDoctor(newDoctor);

            Doctor doctor = FromRequest(newDoctor, 0);

            _doctorRepo.AddNewDoctor(doctor);

            return DoctorResponse.FromModel(doctor, false, false);
        }

        public DoctorResponse UpdateDoctor(UpdateDoctorRequest doctor, int doctorId)
        {
            IsValidDoctor(doctor, doctorId);

            Doctor doctorModel = FromRequest(doctor, doctorId);

            _doctorRepo.UpdateDoctor(doctorModel, doctorId);

            return DoctorResponse.FromModel(doctorModel, true, true);
        }

        public DoctorResponse DeleteDoctorById(int doctorId)
        {
            var doctor = _doctorRepo.GetDoctorById(doctorId);

            if (doctor is null)
                throw new ArgumentNullException("Doctor Does Not Exist");

            _doctorRepo.DeleteDoctorById(doctorId);

            return DoctorResponse.FromModel(doctor, true, true);
        }

        public List<PatientResponse> GetDoctorPatients(int doctorId, bool includeAppointments)
        {
            var doctor = _doctorRepo.GetDoctorById(doctorId);

            if (doctor is null)
                throw new ArgumentNullException("Doctor Does Not Exist");

            var patients = _patientRepo.GetPatientByDoctor(doctor);

            if (patients is null)
                return [];

            return PatientResponse.FromModels(patients, includeAppointments)!.ToList();
        }

        public List<AppointmentResponse> GetDoctorAppointments(int doctorId)
        {
            var doctor = _doctorRepo.GetDoctorById(doctorId);

            if (doctor is null)
                throw new ArgumentNullException("Doctor Does Not Exist");

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

        private void IsValidDoctor(DoctorRequest doctorRequest, int doctorId = 0)
        {
            if (doctorId > 0)
            {
                var doctor = _doctorRepo.GetDoctorById(doctorId);
                if (doctor is null)
                    throw new ArgumentNullException("Doctor Does Not Exist");
            }

            if (!doctorRequest.FirstName.All(char.IsLetter) || !doctorRequest.LastName.All(char.IsLetter))
                throw new ArgumentException("Name Must Contain Only Letters");

            if (doctorRequest.Age < 0 || doctorRequest.Age > 120)
                throw new ArgumentException("Invalid Age");

            if (doctorRequest.YearOfExperience < 0 || doctorRequest.YearOfExperience > 100)
                throw new ArgumentException("Invalid Years Of Experience");

            if (!doctorRequest.Phone.All(char.IsDigit) || doctorRequest.Phone.Count() < 10 || doctorRequest.Phone.Count() > 12)
                throw new ArgumentException("Invalid Phone Number");
        }

        private Doctor FromRequest(DoctorRequest doctorRequest, int doctorId)
        {
            var doctor = new Doctor
            {
                FirstName = doctorRequest.FirstName,
                LastName = doctorRequest.LastName,
                Specialist = doctorRequest.Specialist,
                Phone = doctorRequest.Phone,
                Age = doctorRequest.Age,
                YearOfExperience = doctorRequest.YearOfExperience
            };

            if (doctorRequest is UpdateDoctorRequest)
                doctor.DoctorId = doctorId;

            return doctor;
        }
    }
}
