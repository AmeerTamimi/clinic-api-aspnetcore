using ClinicAPI.Enums;
using ClinicAPI.Models;
using ClinicAPI.Query;
using System.Text.Json.Serialization;

namespace ClinicAPI.Responses
{
    public class DoctorResponse : UserResponse
    {
        [JsonPropertyOrder(100)] public DoctorSpecialty Specialty { get; set; }
        [JsonPropertyOrder(101)] public decimal Salary { get; set; }
        [JsonPropertyOrder(102)] public int YearOfExperience { get; set; }

        [JsonPropertyOrder(103)] public List<AppointmentResponse> Appointments { get; set; } = new();
        [JsonPropertyOrder(104)] public List<PatientResponse> Patients { get; set; } = new();

        private DoctorResponse() { }

        public static DoctorResponse FromModel(Doctor doctor, bool includePatients, bool includeAppointments)
        {
            var doctorResponse = new DoctorResponse
            {
                UserId = doctor.UserId,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Age = doctor.Age,
                Phone = doctor.Phone,
                Email = doctor.Email,

                Specialty = doctor.Specialty,
                Salary = doctor.Salary,
                YearOfExperience = doctor.YearOfExperience
            };

            if (includePatients && doctor.DoctorPatients is not null)
            {
                doctorResponse.Patients = doctor.DoctorPatients
                    .Select(p => PatientResponse.FromModel(p, includeAppointments: false))
                    .ToList();
            }

            if (includeAppointments && doctor.DoctorAppointments is not null)
            {
                doctorResponse.Appointments = doctor.DoctorAppointments
                    .Select(AppointmentResponse.FromModel)
                    .ToList();
            }

            return doctorResponse;
        }

        public static IEnumerable<DoctorResponse>? FromModels(IEnumerable<Doctor>? doctors, DoctorQuery query)
        { 
            return doctors?.Select(d => FromModel(d, query.IncludePatients, query.IncludeAppointments)); ;
        }
    }
}
