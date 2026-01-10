using ClinicAPI.Enums;
using ClinicAPI.Models;
using ClinicAPI.Query;

namespace ClinicAPI.Responses
{
    public class PatientResponse : UserResponse
    {
        public RiskLevel RiskLevel { get; set; }
        public BloodType BloodType { get; set; }
        public string Allergies { get; set; }
        public string Note { get; set; }
        public int DoctorId { get; set; }
        public List<AppointmentResponse>? Appointments { get; set; }
        private PatientResponse() { }

        public static PatientResponse FromModel(Patient patient , bool includeAppointments)
        {
            var patientResponse = new PatientResponse();
            patientResponse.UserId = patient.UserId;
            patientResponse.FirstName = patient.FirstName;
            patientResponse.LastName = patient.LastName;
            patientResponse.Age = patient.Age;
            patientResponse.DoctorId = patient.DoctorId;
            
            if(patient.PatientAppointments != null && includeAppointments)
            {
                patientResponse.Appointments = patient.PatientAppointments.Where(a => a.PatientUserId == patientResponse.UserId)
                                                                   .Select(a => AppointmentResponse.FromModel(a))
                                                                   .ToList();
            }

            return patientResponse;
        }

        public static IEnumerable<PatientResponse>? FromModels(IEnumerable<Patient>? patients, PatientQuery query)
        {
            if (patients == null)
                return null;

            return patients.Select(p => FromModel(p , query.IncludeAppointments));
        }
    }
}
