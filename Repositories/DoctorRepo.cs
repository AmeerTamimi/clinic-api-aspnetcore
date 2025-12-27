using ClinicAPI.Models;
using ClinicAPI.Presistence;
using Microsoft.EntityFrameworkCore;

namespace ClinicAPI.Repositories
{
    public class DoctorRepo(ClinicDbContext context) : IDoctorRepo
    {
        public async Task<Doctor?> GetDoctorByIdAsync(int doctorId)
        {
            return await context.Doctors
                .AsNoTracking()
                .Include(d => d.DoctorAppointments)
                .Include(d => d.DoctorPatients)
                .SingleOrDefaultAsync(d => d.DoctorId == doctorId && !d.IsDeleted);
        }

        public async Task<Doctor> AddNewDoctorAsync(Doctor newDoctor)
        {
            newDoctor.CreatedAt = DateTimeOffset.UtcNow;
            newDoctor.IsDeleted = false;

            await context.Doctors.AddAsync(newDoctor);
            await context.SaveChangesAsync();

            return newDoctor;
        }

        public async Task UpdateDoctorAsync(Doctor doctor, int doctorId)
        {
            var toUpdate = await context.Doctors
                .FirstOrDefaultAsync(d => d.DoctorId == doctorId && !d.IsDeleted);

            toUpdate.FirstName = doctor.FirstName;
            toUpdate.LastName = doctor.LastName;
            toUpdate.Specialty = doctor.Specialty;
            toUpdate.Phone = doctor.Phone;
            toUpdate.Age = doctor.Age;
            toUpdate.YearOfExperience = doctor.YearOfExperience;

            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteDoctorByIdAsync(int doctorId)
        {
            var toDelete = await context.Doctors
                .FirstOrDefaultAsync(d => d.DoctorId == doctorId && !d.IsDeleted);

            if (toDelete is null) return false;

            toDelete.IsDeleted = true;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetDoctorCountAsync()
        {
            return await context.Doctors.CountAsync(d => !d.IsDeleted);
        }

        public async Task<List<Doctor>> GetDoctorPageAsync(int page, int pageSize)
        {
            return await context.Doctors
                .AsNoTracking()
                .Where(d => !d.IsDeleted)
                .Include(d => d.DoctorAppointments)
                .Include(d => d.DoctorPatients)
                .OrderBy(d => d.FirstName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentAsync(int doctorId)
        {
            return await context.Appointments
                .AsNoTracking()
                .Where(a => !a.IsDeleted && a.DoctorId == doctorId)
                .OrderBy(a => a.Date)
                .ToListAsync();
        }
    }
}
