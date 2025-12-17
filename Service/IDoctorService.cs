using ClinicAPI.Requests;
using ClinicAPI.Responses;

namespace ClinicAPI.Service
{
    public interface IDoctorService
    {
        DoctorResponse AddNewDoctor(CreateDoctorRequest NewDoctor);
        DoctorResponse UpdateDoctor(UpdateDoctorRequest Doctor , int DoctorId);
    }
}
