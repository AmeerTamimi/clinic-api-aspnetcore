
using ClinicAPI.Models;

namespace ClinicAPI.Requests
{
    public class UpdatePatientRequest : PatientRequest
    {
        public List<Appointment>? Appointments { get; set; }
    }
}
