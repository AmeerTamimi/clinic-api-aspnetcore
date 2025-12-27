using ClinicAPI.Enums;
using ClinicAPI.Models;
using ClinicAPI.Query;
using System.Collections.Generic;
using System.Linq;

namespace ClinicAPI.Responses
{
    public class DoctorResponse
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DoctorSpecialty Specialty { get; set; } = default!;
        public int Age { get; set; }
        public string Phone { get; set; }
        public int YearOfExperience { get; set; }
        public List<AppointmentResponse>? Appointments { get; set; }
        public List<PatientResponse>? Patients { get; set; }

        private DoctorResponse() { }

        public static DoctorResponse FromModel(Doctor doctor, bool includePatients, bool includeAppointments)
        {
            var doctorResponse = new DoctorResponse();
            doctorResponse.DoctorId = doctor.DoctorId;
            doctorResponse.FirstName = doctor.FirstName;
            doctorResponse.LastName = doctor.LastName;
            doctorResponse.Specialty = doctor.Specialty;
            doctorResponse.Age = doctor.Age;
            doctorResponse.YearOfExperience = doctor.YearOfExperience;
            doctorResponse.Phone = doctor.Phone!;

            if (doctor.DoctorPatients != null && includePatients)
            {
                doctorResponse.Patients = doctor.DoctorPatients.Select(p => PatientResponse.FromModel(p , false)).ToList();
            }
            if (doctor.DoctorAppointments != null && includeAppointments)
            {
                Console.WriteLine($"LEngth : : : :: :{doctor.DoctorAppointments.Count()}");
                doctorResponse.Appointments = doctor.DoctorAppointments.Where(a => a.DoctorId == doctorResponse.DoctorId)
                                                                   .Select(a => AppointmentResponse.FromModel(a))
                                                                   .ToList();
            }

            return doctorResponse;
        }

        public static IEnumerable<DoctorResponse>? FromModels(IEnumerable<Doctor>? doctors, DoctorQuery query)
        {
            if (doctors == null)
                return null;

            return doctors.Select(d => FromModel(d, query.IncludePatients, query.IncludeAppointments));
        }
    }
}
