using ClinicAPI.Models;

namespace ClinicAPI.Requests
{
    public class CreateDoctorRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialist { get; set; }
        public int Age { get; set; }
        public int YearOfExperience { get; set; }
    }
}
