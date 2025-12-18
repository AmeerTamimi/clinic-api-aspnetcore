using ClinicAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ClinicAPI.Responses
{
    public class PatientResponse
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; } = default!;
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string? Symptoms { get; set; }
        public string? Medicine { get; set; }
        public string? Diagnostic { get; set; }
        public int DoctorId { get; set; }
        public List<AppointmentResponse>? Appointments { get; set; }

        private PatientResponse() { }

        public static PatientResponse FromModel(Patient patient)
        {
            var patientResponse = new PatientResponse();
            patientResponse.PatientId = patient.PatientId;
            patientResponse.FirstName = patient.FirstName;
            patientResponse.LastName = patient.LastName;
            patientResponse.Age = patient.Age;
            patientResponse.Symptoms = patient.Symptoms;
            patientResponse.Medicine = patient.Medicine;
            patientResponse.Diagnostic = patient.Diagnostic;
            patientResponse.DoctorId = patient.DoctorId;

            if(patient.Appointments != null)
            {
                patientResponse.Appointments =patient.Appointments.Select(a => AppointmentResponse.FromModel(a)).ToList();
            }

            return patientResponse;
        }

        public static IEnumerable<PatientResponse>? FromModels(IEnumerable<Patient>? patients)
        {
            if (patients == null)
                return null;

            return patients.Select(p => FromModel(p));
        }
    }
}
