using ClinicAPI.Enums;

namespace ClinicAPI.Requests
{
    public class CreatePatientRequest : UserRequest
    {
        public RiskLevel RiskLevel { get; set; }
        public BloodType BloodType { get; set; }
        public string? Allergies { get; set; }
        public string? Note { get; set; }
        public int DoctorId { get; set; }
    }
}
