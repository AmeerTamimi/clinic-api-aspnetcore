using ClinicAPI.Models;

namespace ClinicAPI.Requests
{
    public class UpdateDoctorRequest : DoctorRequest
    {
        public List<int>? Patients { get; set; }
        public List<int>? Appointments { get; set; }
    }
}
