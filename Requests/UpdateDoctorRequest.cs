using ClinicAPI.Enums;
using ClinicAPI.Models;

namespace ClinicAPI.Requests
{
    public class UpdateDoctorRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DoctorSpecialty Specialty { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public int YearOfExperience { get; set; }
    }
}
