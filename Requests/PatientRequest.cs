namespace ClinicAPI.Requests
{
    public class PatientRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string? Symptoms { get; set; }
        public string? Medicine { get; set; }
        public string? Diagnostic { get; set; }
        public int DoctorId { get; set; }
    }
}
