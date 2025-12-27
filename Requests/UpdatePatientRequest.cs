
using ClinicAPI.Responses;

namespace ClinicAPI.Requests
{
    public class UpdatePatientRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int DoctorId { get; set; }
    }
}
