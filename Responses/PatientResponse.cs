using ClinicAPI.Enums;
using ClinicAPI.Models;
using ClinicAPI.Query;
using Microsoft.AspNetCore.DataProtection;
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

        public static PatientResponse FromModel(Patient patient, bool includeAppointments , IDataProtector protector)
        {
            var patientResponse = new PatientResponse
            {
                UserId = patient.UserId,
                FirstName = protector.Unprotect(patient.FirstName),
                LastName = protector.Unprotect(patient.LastName),
                Age = patient.Age,
                Phone = protector.Unprotect(patient.Phone),
                Email = protector.Unprotect(patient.Email),

                RiskLevel = patient.RiskLevel,
                BloodType = patient.BloodType,
                Allergies = protector.Unprotect(patient.Allergies!),
                Note = protector.Unprotect(patient.Note!),
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

        public static IEnumerable<PatientResponse>? FromModels(IEnumerable<Patient>? patients, PatientQuery query , IDataProtector protector)
        {
            return patients?.Select(p => FromModel(p, query.IncludeAppointments , protector));
        }
    }
}
