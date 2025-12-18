namespace ClinicAPI.Requests
{
    public class DoctorRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialist { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public int YearOfExperience { get; set; }
    }
}
