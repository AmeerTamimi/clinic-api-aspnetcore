using ClinicAPI.Models;
using ClinicAPI.Presistence;
using Microsoft.EntityFrameworkCore;

namespace ClinicAPI.Repositories
{
    public class AppointmentRepo(ClinicDbContext context) : IAppointmentRepo
    {
        public async Task<Appointment?> GetAppointmentByIdAsync(int appointmentId)
        {
            return await context.Appointments
                .SingleOrDefaultAsync(a => a.AppointmentId == appointmentId && !a.IsDeleted);
        }

        public async Task<Appointment> AddNewAppointmentAsync(Appointment newAppointment)
        {
            newAppointment.CreatedAt = DateTimeOffset.UtcNow;
            newAppointment.IsDeleted = false;

            await context.Appointments.AddAsync(newAppointment);
            await context.SaveChangesAsync();

            return newAppointment;
        }

        public async Task UpdateAppointmentAsync(Appointment appointment, int appointmentId)
        {
            var toUpdate = await context.Appointments
                .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId && !a.IsDeleted);

            toUpdate.DoctorId = appointment.DoctorId;
            toUpdate.PatientId = appointment.PatientId;
            toUpdate.Date = appointment.Date;
            toUpdate.Symptoms = appointment.Symptoms;
            toUpdate.Diagnostic = appointment.Diagnostic ?? toUpdate.Diagnostic;
            toUpdate.Medicine = appointment.Medicine ?? toUpdate.Medicine;
            toUpdate.IsDone = appointment.IsDone;

            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAppointmentByIdAsync(int appointmentId)
        {
            var toDelete = await context.Appointments
                .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId && !a.IsDeleted);

            if (toDelete is null) return false;

            toDelete.IsDeleted = true;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetAppointmentCountAsync()
        {
            return await context.Appointments.CountAsync(a => !a.IsDeleted);
        }

        public async Task<List<Appointment>> GetAppointmentPageAsync(int page, int pageSize)
        {
            return await context.Appointments
                .AsNoTracking()
                .Where(a => !a.IsDeleted)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentByDoctorIdAsync(int doctorId)
        {
            return await context.Appointments
                .AsNoTracking()
                .Where(a => !a.IsDeleted && a.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentByPatientIdAsync(int patientId)
        {
            return await context.Appointments
                .AsNoTracking()
                .Where(a => !a.IsDeleted && a.PatientId == patientId)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentByDateAsync(DateTimeOffset from, DateTimeOffset to)
        {
            return await context.Appointments
                .AsNoTracking()
                .Where(a => !a.IsDeleted && a.Date >= from && a.Date <= to)
                .ToListAsync();
        }

        public async Task<bool> HasAppointmentConflictAsync(Appointment appointment)
        {
            return await context.Appointments
                .AnyAsync(a => !a.IsDeleted && !a.IsDone
                    && (a.DoctorId == appointment.DoctorId || a.PatientId == appointment.PatientId)
                    && a.Date == appointment.Date);
        }
    }
}
