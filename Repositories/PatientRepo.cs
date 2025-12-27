using ClinicAPI.Models;
using ClinicAPI.Presistence;
using Microsoft.EntityFrameworkCore;

namespace ClinicAPI.Repositories
{
    public class PatientRepo(ClinicDbContext context) : IPatientRepo
    {
        public async Task<Patient?> GetPatientByIdAsync(int patientId)
        {
            return await context.Patients
                .AsNoTracking()
                .Include(p => p.PatientAppointments)
                .SingleOrDefaultAsync(p => !p.IsDeleted && p.PatientId == patientId);
        }

        public async Task<Patient> AddNewPatientAsync(Patient newPatient)
        {
            newPatient.CreatedAt = DateTimeOffset.UtcNow;
            newPatient.IsDeleted = false;

            await context.Patients.AddAsync(newPatient);
            await context.SaveChangesAsync();

            return newPatient;
        }

        public async Task<bool> UpdatePatientAsync(Patient updatedPatient)
        {
            var patientToUpdate = await context.Patients
                .FirstOrDefaultAsync(p => !p.IsDeleted && p.PatientId == updatedPatient.PatientId);

            if (patientToUpdate is null)
                return false;

            patientToUpdate.FirstName = updatedPatient.FirstName;
            patientToUpdate.LastName = updatedPatient.LastName;
            patientToUpdate.Age = updatedPatient.Age;
            patientToUpdate.DoctorId = updatedPatient.DoctorId;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePatientByIdAsync(int patientId)
        {
            var toDelete = await context.Patients.FirstOrDefaultAsync(p => p.PatientId == patientId && !p.IsDeleted);
            if (toDelete is null) return false;

            toDelete.IsDeleted = true;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Patient>> GetPatientByDoctorAsync(Doctor doctor)
        {
            return await context.Patients
                .AsNoTracking()
                .Include(p => p.PatientAppointments)
                .Where(p => !p.IsDeleted && p.DoctorId == doctor.DoctorId)
                .ToListAsync();
        }

        public async Task<int> GetPatientCountAsync()
        {
            return await context.Patients.CountAsync(p => !p.IsDeleted);
        }

        public async Task<List<Patient>> GetPatientPageAsync(int page, int pageSize)
        {
            return await context.Patients
                .AsNoTracking()
                .Where(p => !p.IsDeleted)
                .Include(p => p.PatientAppointments)
                .OrderBy(p => p.PatientId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
