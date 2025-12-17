using ClinicAPI.Models;
using ClinicAPI.Responses;

namespace ClinicAPI.Repositories
{
    public interface IDoctorRepo
    {
        Doctor GetDoctorById(int DoctorId);
        Doctor AddNewDoctor(Doctor NewDoctor);
        Doctor UpdateDoctor(Doctor Doctor , int DoctorId);
    }
}
