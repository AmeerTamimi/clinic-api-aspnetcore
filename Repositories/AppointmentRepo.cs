using ClinicAPI.Models;
using ClinicAPI.Presistence;
using Microsoft.EntityFrameworkCore;

namespace ClinicAPI.Repositories
{
    public class AppointmentRepo(ClinicDbContext context) : IAppointmentRepo
    {
        public async Task<Appointment?> GetAppointmentByIdAsync(int appointmentId, CancellationToken ct = default)
        {
            return await context.Appointments
                .SingleOrDefaultAsync(a => a.AppointmentId == appointmentId && !a.IsDeleted, ct);
        }

        public async Task<Appointment> AddNewAppointmentAsync(Appointment newAppointment, CancellationToken ct = default)
        {
            newAppointment.CreatedAt = DateTimeOffset.UtcNow;
            newAppointment.IsDeleted = false;

            await context.Appointments.AddAsync(newAppointment, ct);
            await context.SaveChangesAsync(ct);

            return newAppointment;
        }

        public async Task UpdateAppointmentAsync(Appointment appointment, int appointmentId, CancellationToken ct = default)
        {
            var toUpdate = await context.Appointments
                .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId && !a.IsDeleted, ct);

            toUpdate.DoctorUserId = appointment.DoctorUserId;
            toUpdate.PatientUserId = appointment.PatientUserId;
            toUpdate.Date = appointment.Date;
            toUpdate.Symptoms = appointment.Symptoms;
            toUpdate.Diagnostic = appointment.Diagnostic ?? toUpdate.Diagnostic;
            toUpdate.Medicine = appointment.Medicine ?? toUpdate.Medicine;
            toUpdate.IsDone = appointment.IsDone;

            await context.SaveChangesAsync(ct);
        }

        public async Task<bool> DeleteAppointmentByIdAsync(int appointmentId, CancellationToken ct = default)
        {
            var toDelete = await context.Appointments
                .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId && !a.IsDeleted, ct);

            if (toDelete is null) return false;

            toDelete.IsDeleted = true;
            await context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<int> GetAppointmentCountAsync(CancellationToken ct = default)
        {
            return await context.Appointments.CountAsync(a => !a.IsDeleted, ct);
        }

        public async Task<List<Appointment>> GetAppointmentPageAsync(int page, int pageSize, CancellationToken ct = default)
        {
            return await context.Appointments
                .AsNoTracking()
                .Where(a => !a.IsDeleted)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
        }

        public async Task<List<Appointment>> GetAppointmentByDoctorIdAsync(int doctorId, CancellationToken ct = default)
        {
            return await context.Appointments
                .AsNoTracking()
                .Where(a => !a.IsDeleted && a.DoctorUserId == doctorId)
                .ToListAsync(ct);
        }

        public async Task<List<Appointment>> GetAppointmentByPatientIdAsync(int patientId, CancellationToken ct = default)
        {
            return await context.Appointments
                .AsNoTracking()
                .Where(a => !a.IsDeleted && a.PatientUserId == patientId)
                .ToListAsync(ct);
        }

        public async Task<List<Appointment>> GetAppointmentByDateAsync(DateTimeOffset from, DateTimeOffset to, CancellationToken ct = default)
        {
            return await context.Appointments
                .AsNoTracking()
                .Where(a => !a.IsDeleted && a.Date >= from && a.Date <= to)
                .ToListAsync(ct);
        }

        public async Task<bool> HasAppointmentConflictAsync(Appointment appointment, CancellationToken ct = default)
        {
            return await context.Appointments
                .AnyAsync(a => !a.IsDeleted && !a.IsDone
                    && (a.DoctorUserId == appointment.DoctorUserId || a.PatientUserId == appointment.PatientUserId)
                    && a.Date == appointment.Date, ct);
        }
    }
}
