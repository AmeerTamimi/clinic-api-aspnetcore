using ClinicAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ClinicAPI.Responses
{
    public class DoctorResponse
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Specialist { get; set; } = default!;
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
            doctorResponse.Specialist = doctor.Specialist;
            doctorResponse.Age = doctor.Age;
            doctorResponse.YearOfExperience = doctor.YearOfExperience;
            doctorResponse.Phone = doctor.Phone!;

            if (doctor.Patients != null && includePatients)
            {
                doctorResponse.Patients = doctor.Patients.Select(p => PatientResponse.FromModel(p , false)).ToList();
            }
            if (doctor.Appointments != null && includeAppointments)
            {
                doctorResponse.Appointments = doctor.Appointments.Select(a => AppointmentResponse.FromModel(a)).ToList();
            }

            return doctorResponse;
        }

        public static IEnumerable<DoctorResponse>? FromModels(IEnumerable<Doctor>? doctors, bool includePatients, bool includeAppointments)
        {
            if (doctors == null)
                return null;

            return doctors.Select(d => FromModel(d, includePatients, includeAppointments));
        }
    }
}
