using ClinicAPI.Models;
using ClinicAPI.Presistence;
using Microsoft.EntityFrameworkCore;

namespace ClinicAPI.Repositories
{
    public class PatientRepo(ClinicDbContext context) : IPatientRepo
    {
        public Patient GetPatientById(int patientId)
        {
                return context.Patients
                    .AsNoTracking()
                    .Include(p => p.PatientAppointments)
                    .SingleOrDefault(p => !p.IsDeleted && p.PatientId == patientId)!;
        }

        public Patient AddNewPatient(Patient newPatient)
        {
            newPatient.CreatedAt = DateTimeOffset.UtcNow;
            newPatient.IsDeleted = false;

            context.Patients.Add(newPatient);
            context.SaveChanges();

            return newPatient;
        }

        public bool UpdatePatient(Patient updatedPatient)
        {
            var patientToUpdate = context.Patients
                .FirstOrDefault(p => !p.IsDeleted && p.PatientId == updatedPatient.PatientId);

            if (patientToUpdate is null)
                return false;

            patientToUpdate.FirstName = updatedPatient.FirstName;
            patientToUpdate.LastName = updatedPatient.LastName;
            patientToUpdate.Age = updatedPatient.Age;
            patientToUpdate.DoctorId = updatedPatient.DoctorId;

            context.SaveChanges();
            return true;
        }

        public bool DeletePatientById(int patientId)
        {
            var toDelete = context.Patients.FirstOrDefault(p => p.PatientId == patientId && !p.IsDeleted);
            if (toDelete is null) return false;

            toDelete.IsDeleted = true;
            context.SaveChanges();
            return true;
        }

        public List<Patient> GetPatientByDoctor(Doctor doctor)
        {
            return context.Patients
                .AsNoTracking()
                .Include(p => p.PatientAppointments)
                .Where(p => !p.IsDeleted && p.DoctorId == doctor.DoctorId)
                .ToList();
        }

        public int GetPatientCount()
        {
            return context.Patients.Count(p => !p.IsDeleted);
        }

        public List<Patient> GetPatientPage(int page, int pageSize)
        {
            return context.Patients
                .AsNoTracking()
                .Where(p => !p.IsDeleted)
                .Include(p => p.PatientAppointments)
                .OrderBy(p => p.PatientId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
