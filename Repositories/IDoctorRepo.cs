using ClinicAPI.Models;

namespace ClinicAPI.Repositories
{
    public interface IDoctorRepo
    {
        Doctor? GetDoctorById(int doctorId);
        Doctor AddNewDoctor(Doctor newDoctor);
        void UpdateDoctor(Doctor doctor , int doctorId);
        bool DeleteDoctorById(int doctorId);
        int GetDoctorCount();
        List<Doctor> GetDoctorPage(int page, int pageSize);
    }
}
