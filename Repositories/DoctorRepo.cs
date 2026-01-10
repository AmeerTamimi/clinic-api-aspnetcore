using ClinicAPI.Models;
using ClinicAPI.Presistence;
using Microsoft.EntityFrameworkCore;

namespace ClinicAPI.Repositories
{
    public class DoctorRepo(ClinicDbContext context) : IDoctorRepo
    {
        public async Task<Doctor?> GetDoctorByIdAsync(int doctorId, CancellationToken ct = default)
        {
            return await context.Doctors
                .AsNoTracking()
                .Include(d => d.DoctorAppointments)
                .Include(d => d.DoctorPatients)
                .SingleOrDefaultAsync(d => d.UserId == doctorId && !d.IsDeleted, ct);
        }

        public async Task<Doctor> AddNewDoctorAsync(Doctor newDoctor, CancellationToken ct = default)
        {
            newDoctor.CreatedAt = DateTimeOffset.UtcNow;
            newDoctor.IsDeleted = false;

            await context.Doctors.AddAsync(newDoctor, ct);
            await context.SaveChangesAsync(ct);

            return newDoctor;
        }

        public async Task UpdateDoctorAsync(Doctor doctor, int doctorId, CancellationToken ct = default)
        {
            var toUpdate = await context.Doctors
                .FirstOrDefaultAsync(d => d.UserId == doctorId && !d.IsDeleted, ct);

            toUpdate.FirstName = doctor.FirstName;
            toUpdate.LastName = doctor.LastName;
            toUpdate.Specialty = doctor.Specialty;
            toUpdate.Phone = doctor.Phone;
            toUpdate.Age = doctor.Age;
            toUpdate.YearOfExperience = doctor.YearOfExperience;

            await context.SaveChangesAsync(ct);
        }

        public async Task<bool> DeleteDoctorByIdAsync(int doctorId, CancellationToken ct = default)
        {
            var toDelete = await context.Doctors
                .FirstOrDefaultAsync(d => d.UserId == doctorId && !d.IsDeleted, ct);

            if (toDelete is null) return false;

            toDelete.IsDeleted = true;
            await context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<int> GetDoctorCountAsync(CancellationToken ct = default)
        {
            return await context.Doctors.CountAsync(d => !d.IsDeleted, ct);
        }

        public async Task<List<Doctor>> GetDoctorPageAsync(int page, int pageSize, CancellationToken ct = default)
        {
            return await context.Doctors
                .AsNoTracking()
                .Where(d => !d.IsDeleted)
                .Include(d => d.DoctorAppointments)
                .Include(d => d.DoctorPatients)
                .OrderBy(d => d.FirstName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
        }

        public async Task<List<Appointment>> GetAppointmentAsync(int doctorId, CancellationToken ct = default)
        {
            return await context.Appointments
                .AsNoTracking()
                .Where(a => !a.IsDeleted && a.DoctorUserId == doctorId)
                .OrderBy(a => a.Date)
                .ToListAsync(ct);
        }
    }
}
