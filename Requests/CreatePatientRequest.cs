
namespace ClinicAPI.Requests
{
    public class CreatePatientRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int DoctorId { get; set; }
    }
}
