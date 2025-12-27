using ClinicAPI.Models;
using ClinicAPI.Requests;
using System.Collections.Generic;

namespace ClinicAPI.Repositories
{
    public class PatientRepo : IPatientRepo
    {
        private static DateTimeOffset now = DateTimeOffset.UtcNow;

        private static List<Patient> _patients = new List<Patient>
            {
                new() { PatientId = 1,  FirstName = "Ameer",  LastName = "Tamimi",   Age = 20, DoctorId = 7, CreatedAt = now.AddDays(-14), IsDeleted = false },
                new() { PatientId = 2,  FirstName = "Hareth", LastName = "Shoman",   Age = 21, DoctorId = 2, CreatedAt = now.AddDays(-13), IsDeleted = false },
                new() { PatientId = 3,  FirstName = "Elyas",  LastName = "Najeh",    Age = 22, DoctorId = 3, CreatedAt = now.AddDays(-12), IsDeleted = false },
                new() { PatientId = 4,  FirstName = "Mariam", LastName = "Yasin",    Age = 30, DoctorId = 5, CreatedAt = now.AddDays(-11), IsDeleted = false },
                new() { PatientId = 5,  FirstName = "Kareem", LastName = "AbuLail",  Age = 28, DoctorId = 1, CreatedAt = now.AddDays(-10), IsDeleted = false },
                new() { PatientId = 6,  FirstName = "Noor",   LastName = "Said",     Age = 19, DoctorId = 8, CreatedAt = now.AddDays(-9),  IsDeleted = false },
                new() { PatientId = 7,  FirstName = "Zaid",   LastName = "Qamhieh",  Age = 23, DoctorId = 4, CreatedAt = now.AddDays(-8),  IsDeleted = false },
                new() { PatientId = 8,  FirstName = "Habeeb", LastName = "Ahmad",    Age = 24, DoctorId = 3, CreatedAt = now.AddDays(-7),  IsDeleted = false },
                new() { PatientId = 9,  FirstName = "Waleed", LastName = "Noubani",  Age = 26, DoctorId = 7, CreatedAt = now.AddDays(-6),  IsDeleted = false },
                new() { PatientId = 10, FirstName = "Ruba",   LastName = "Katout",   Age = 29, DoctorId = 6, CreatedAt = now.AddDays(-5),  IsDeleted = false },
            };

        public Patient GetPatientById(int patientId)
        {
            var patient = _patients.FirstOrDefault(p => !p.IsDeleted && p.PatientId == patientId);
            return patient;
        }

        public Patient AddNewPatient(Patient newPatient)
        {
            int newId = _patients.Count == 0 ? 1 : _patients.Max(a => a.PatientId) + 1;

            newPatient.PatientId = newId;
            newPatient.CreatedAt = DateTimeOffset.UtcNow;
            newPatient.IsDeleted = false;

            _patients.Add(newPatient);

            return newPatient;
        }

        public bool UpdatePatient(Patient updatedPatient)
        {
            var patientToUpdate = _patients.FirstOrDefault(p => p.PatientId == updatedPatient.PatientId);

            if (patientToUpdate is null)
                return false;

            patientToUpdate.FirstName = updatedPatient.FirstName;
            patientToUpdate.LastName = updatedPatient.LastName;
            patientToUpdate.Age = updatedPatient.Age;            
            patientToUpdate.Appointments = updatedPatient.Appointments;
            patientToUpdate.DoctorId = updatedPatient.DoctorId;

            return true;
        }

        public bool DeletePatientById(int patientId)
        {
            var toDelete = _patients.FirstOrDefault(p => p.PatientId == patientId && !p.IsDeleted)!;

            if (toDelete is null)
                return false;

            toDelete.IsDeleted = true;

            return true;
        }

        public List<Patient> GetPatientByDoctor(Doctor doctor)
        {
            return _patients.Where(p => !p.IsDeleted && p.DoctorId == doctor.DoctorId).ToList();
        }

        public int GetPatientCount()
        {
            return _patients.Count(p => !p.IsDeleted);
        }

        public List<Patient> GetPatientPage(int page, int pageSize)
        {
            return _patients.Where(p => !p.IsDeleted)
                            .OrderBy(p => p.PatientId)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
        }
    }
}