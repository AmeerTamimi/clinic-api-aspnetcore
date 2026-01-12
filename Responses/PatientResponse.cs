using ClinicAPI.Enums;
using ClinicAPI.Models;
using ClinicAPI.Query;
using System.Text.Json.Serialization;

namespace ClinicAPI.Responses
{
    public class PatientResponse : UserResponse
    {
        [JsonPropertyOrder(100)] public RiskLevel? RiskLevel { get; set; }
        [JsonPropertyOrder(101)] public BloodType? BloodType { get; set; }
        [JsonPropertyOrder(102)] public string? Allergies { get; set; }
        [JsonPropertyOrder(103)] public string? Note { get; set; }

        [JsonPropertyOrder(104)] public int DoctorId { get; set; }
        [JsonPropertyOrder(105)] public List<AppointmentResponse> Appointments { get; set; } = new();

        private PatientResponse() { }

        public static PatientResponse FromModel(Patient patient, bool includeAppointments)
        {
            var patientResponse = new PatientResponse
            {
                UserId = patient.UserId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Age = patient.Age,
                Phone = patient.Phone,
                Email = patient.Email,

                RiskLevel = patient.RiskLevel,
                BloodType = patient.BloodType,
                Allergies = patient.Allergies,
                Note = patient.Note,
                DoctorId = patient.DoctorId
            };

            if (includeAppointments && patient.PatientAppointments is not null)
            {
                patientResponse.Appointments = patient.PatientAppointments
                    .Select(AppointmentResponse.FromModel)
                    .ToList();
            }

            return patientResponse;
        }

        public static IEnumerable<PatientResponse>? FromModels(IEnumerable<Patient>? patients, PatientQuery query)
        {
            return patients?.Select(p => FromModel(p, query.IncludeAppointments));
        }
    }
}
