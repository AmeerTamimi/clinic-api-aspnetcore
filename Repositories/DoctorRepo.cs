using ClinicAPI.Models;
using ClinicAPI.Presistence;
using Microsoft.EntityFrameworkCore;

namespace ClinicAPI.Repositories
{
    public class DoctorRepo(ClinicDbContext context) : IDoctorRepo
    {
        public Doctor GetDoctorById(int doctorId)
        {
            return context.Doctors
                .AsNoTracking()
                .Include(d => d.DoctorAppointments)
                .Include(d => d.DoctorPatients)
                .FirstOrDefault(d => d.DoctorId == doctorId && !d.IsDeleted)!;
        }

        public Doctor AddNewDoctor(Doctor newDoctor)
        {
            newDoctor.CreatedAt = DateTimeOffset.UtcNow;
            newDoctor.IsDeleted = false;

            context.Doctors.Add(newDoctor);
            context.SaveChanges();

            return newDoctor;
        }

        public void UpdateDoctor(Doctor doctor, int doctorId)
        {
            var toUpdate = context.Doctors.FirstOrDefault(d => d.DoctorId == doctorId && !d.IsDeleted);
            if (toUpdate is null) return;

            toUpdate.FirstName = doctor.FirstName;
            toUpdate.LastName = doctor.LastName;
            toUpdate.Specialty = doctor.Specialty;
            toUpdate.Phone = doctor.Phone;
            toUpdate.Age = doctor.Age;
            toUpdate.YearOfExperience = doctor.YearOfExperience;

            context.SaveChanges();
        }

        public bool DeleteDoctorById(int doctorId)
        {
            var toDelete = context.Doctors.FirstOrDefault(d => d.DoctorId == doctorId && !d.IsDeleted);
            if (toDelete is null) return false;

            toDelete.IsDeleted = true;
            context.SaveChanges();
            return true;
        }

        public int GetDoctorCount()
        {
            return context.Doctors.Count(d => !d.IsDeleted);
        }

        public List<Doctor> GetDoctorPage(int page, int pageSize)
        {
            return context.Doctors
                .AsNoTracking()
                .Where(d => !d.IsDeleted)
                .Include(d => d.DoctorAppointments)
                .Include(d => d.DoctorPatients)
                .OrderBy(d => d.FirstName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public List<Appointment> GetAppointment(int doctorId)
        {
            return context.Appointments
                .AsNoTracking()
                .Where(a => !a.IsDeleted && a.DoctorId == doctorId)
                .OrderBy(a => a.Date)
                .ToList();
        }
    }
}
