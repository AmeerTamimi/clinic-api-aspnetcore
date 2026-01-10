using ClinicAPI.Enums;

namespace ClinicAPI.Requests
{
    public class UpdateDoctorRequest : UserRequest
    {
        public DoctorSpecialty Specialty { get; set; }
        public int YearOfExperience { get; set; }
        public decimal Salary { get; set; }
    }
}
