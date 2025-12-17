using ClinicAPI.Models;
using ClinicAPI.Requests;

namespace ClinicAPI.Repositories
{
    public class DoctorRepo : IDoctorRepo
    {

        public Doctor AddNewDoctor(Doctor NewDoctor)
        {
            // We Add to DB here
            return new Doctor();
        }

        public Doctor GetDoctorById(int DoctorId)
        {
            return new Doctor();
        }

        public Doctor UpdateDoctor(Doctor Doctor , int DoctorId)
        {
            return new Doctor();
        }
    }
}
