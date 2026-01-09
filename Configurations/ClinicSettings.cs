namespace ClinicAPI.Configurations
{
    public class ClinicSettings
    {
        public const string clinicName = "HealthyLifeClinic";
        public TimeSpan OpenAt { get; set; }
        public TimeSpan CloseAt { get;set;}
        public List<string>? OpenDays { get; set; }
        public bool EnabledOnlineReservation { get; set; }
        public int PatientsPerDay { get; set; }
    }
}
