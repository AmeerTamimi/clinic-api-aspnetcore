using ClinicAPI.Repositories;
using ClinicAPI.Requests;
using ClinicAPI.Responses;
using ClinicAPI.Models;

namespace ClinicAPI.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepo _doctorRepo;

        public DoctorService(IDoctorRepo DoctorRepo)
        {
            _doctorRepo = DoctorRepo;
        }

        public DoctorResponse AddNewDoctor(CreateDoctorRequest NewDoctor)
        {
            Doctor doctor = CreateDoctor(NewDoctor);

            _doctorRepo.AddNewDoctor(doctor);

            DoctorResponse doctorResponse = CreateDoctorResponse(doctor);
            return doctorResponse;
        }

        public DoctorResponse UpdateDoctor(UpdateDoctorRequest UpdatedDoctor, int DoctorId)
        {
            if (DoctorId <= 0) throw new ArgumentOutOfRangeException("Doctor Id Must be > 0");
            if (UpdatedDoctor == null) throw new ArgumentNullException(nameof(UpdatedDoctor));

            Doctor CurrentDoctor = _doctorRepo.GetDoctorById(DoctorId);

            if (CurrentDoctor == null) throw new KeyNotFoundException("Doctor Doesnt Exists !");

            // After More Business Validation...

            CurrentDoctor.DoctorId = DoctorId;
            CurrentDoctor.FirstName = UpdatedDoctor.FirstName;
            CurrentDoctor.LastName = UpdatedDoctor.LastName;
            CurrentDoctor.Specialist = UpdatedDoctor.Specialist;
            CurrentDoctor.Age = UpdatedDoctor.Age;
            CurrentDoctor.YearOfExperience = UpdatedDoctor.YearOfExperience;

            _doctorRepo.UpdateDoctor(CurrentDoctor, DoctorId);

            DoctorResponse doctorResponse = CreateDoctorResponse(CurrentDoctor);
            return doctorResponse;
        }

        private Doctor CreateDoctor(CreateDoctorRequest doctor)
        {
            return new Doctor
            {
                DoctorId = 1,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Specialist = doctor.Specialist,
                Age = doctor.Age,
                YearOfExperience = doctor.YearOfExperience,
                Patients = null,
                Appointments = null,
                CreatedAt = DateTimeOffset.Now,
                IsDeleted = false
            };
        }

        private DoctorResponse CreateDoctorResponse(Doctor doctor)
        {
            return new DoctorResponse
            {
                DoctorId = doctor.DoctorId,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Specialist = doctor.Specialist,
                Age = doctor.Age,
                YearOfExperience = doctor.YearOfExperience
            };
        }
    }
}
