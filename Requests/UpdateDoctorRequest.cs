using ClinicAPI.Models;

namespace ClinicAPI.Requests
{
    public class UpdateDoctorRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialist { get; set; }
        public int Age { get; set; }
        public int YearOfExperience { get; set; }
        public List<int>? Patients { get; set; }
        public List<int>? Appointments { get; set; }
    }
}
