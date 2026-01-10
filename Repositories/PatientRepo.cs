using ClinicAPI.Models;
using ClinicAPI.Presistence;
using Microsoft.EntityFrameworkCore;

namespace ClinicAPI.Repositories
{
    public class PatientRepo(ClinicDbContext context) : IPatientRepo
    {
        public async Task<Patient?> GetPatientByIdAsync(int patientId, CancellationToken ct = default)
        {
            return await context.Patients
                .AsNoTracking()
                .Include(p => p.PatientAppointments)
                .SingleOrDefaultAsync(p => !p.IsDeleted && p.UserId == patientId , ct);
        }

        public async Task<Patient> AddNewPatientAsync(Patient newPatient, CancellationToken ct = default)
        {
            newPatient.CreatedAt = DateTimeOffset.UtcNow;
            newPatient.IsDeleted = false;

            await context.Patients.AddAsync(newPatient, ct);
            await context.SaveChangesAsync(ct);

            return newPatient;
        }

        public async Task<bool> UpdatePatientAsync(Patient updatedPatient, CancellationToken ct = default)
        {
            var patientToUpdate = await context.Patients
                .FirstOrDefaultAsync(p => !p.IsDeleted && p.UserId == updatedPatient.UserId, ct);

            if (patientToUpdate is null)
                return false;

            patientToUpdate.FirstName = updatedPatient.FirstName;
            patientToUpdate.LastName = updatedPatient.LastName;
            patientToUpdate.Age = updatedPatient.Age;
            patientToUpdate.DoctorId = updatedPatient.DoctorId;

            await context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeletePatientByIdAsync(int patientId, CancellationToken ct = default)
        {
            var toDelete = await context.Patients.FirstOrDefaultAsync(p => p.UserId == patientId && !p.IsDeleted, ct);
            if (toDelete is null) return false;

            toDelete.IsDeleted = true;
            await context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<List<Patient>> GetPatientByDoctorAsync(Doctor doctor, CancellationToken ct = default)
        {
            return await context.Patients
                .AsNoTracking()
                .Include(p => p.PatientAppointments)
                .Where(p => !p.IsDeleted && p.DoctorId == doctor.UserId)
                .ToListAsync(ct);
        }

        public async Task<int> GetPatientCountAsync(CancellationToken ct = default)
        {
            return await context.Patients.CountAsync(p => !p.IsDeleted , ct);
        }

        public async Task<List<Patient>> GetPatientPageAsync(int page, int pageSize, CancellationToken ct = default)
        {
            return await context.Patients
                .AsNoTracking()
                .Where(p => !p.IsDeleted)
                .Include(p => p.PatientAppointments)
                .OrderBy(p => p.UserId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
        }
    }
}
