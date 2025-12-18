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
            throw new NotImplementedException();
        }

        public DoctorResponse UpdateDoctor(UpdateDoctorRequest Doctor, int DoctorId)
        {
            throw new NotImplementedException();
        }
    }
}
