using ClinicAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicAPI.Repositories
{
    public class AppointmentRepo : IAppointmentRepo
    {
        private static DateTimeOffset now = DateTimeOffset.UtcNow;
        private static TimeSpan tz = TimeSpan.FromHours(2);
        private static Func<int, int, DateTimeOffset> d =
            new Func<int, int, DateTimeOffset>((day, hour) =>
                new DateTimeOffset(2025, 12, day, hour, 0, 0, tz));

        private static List<Appointment> _appointments = new List<Appointment>
        {
            new() { AppointmentId = 1,  DoctorId = 7, PatientId = 1,  Date = d(18,  9),  Symptoms = "Headache", IsDone = false, CreatedAt = now.AddDays(-4), IsDeleted = false },
            new() { AppointmentId = 2,  DoctorId = 2, PatientId = 2,  Date = d(18, 10),  Symptoms = "Skin rash", Diagnostic = "Dermatitis", Medicine = "Cream", IsDone = true,  CreatedAt = now.AddDays(-4), IsDeleted = false },
            new() { AppointmentId = 3,  DoctorId = 3, PatientId = 3,  Date = d(19, 11),  Symptoms = "Knee pain", IsDone = false, CreatedAt = now.AddDays(-3), IsDeleted = false },
            new() { AppointmentId = 4,  DoctorId = 5, PatientId = 4,  Date = d(19, 12),  Symptoms = "Sore throat", IsDone = false, CreatedAt = now.AddDays(-3), IsDeleted = false },
            new() { AppointmentId = 5,  DoctorId = 1, PatientId = 5,  Date = d(20,  9),  Symptoms = "Chest pain", IsDone = false, CreatedAt = now.AddDays(-2), IsDeleted = false },
            new() { AppointmentId = 6,  DoctorId = 8, PatientId = 6,  Date = d(20, 10),  Symptoms = "Blurred vision", Diagnostic = "Dry eye", Medicine = "Eye drops", IsDone = true,  CreatedAt = now.AddDays(-2), IsDeleted = false },
            new() { AppointmentId = 7,  DoctorId = 4, PatientId = 7,  Date = d(21,  9),  Symptoms = "Fever", IsDone = false, CreatedAt = now.AddDays(-1), IsDeleted = false },
            new() { AppointmentId = 8,  DoctorId = 3, PatientId = 8,  Date = d(21, 10),  Symptoms = "Back pain", IsDone = false, CreatedAt = now.AddDays(-1), IsDeleted = false },
            new() { AppointmentId = 9,  DoctorId = 7, PatientId = 9,  Date = d(22, 11),  Symptoms = "Dizziness", IsDone = false, CreatedAt = now,           IsDeleted = false },
            new() { AppointmentId = 10, DoctorId = 6, PatientId = 10, Date = d(22, 12),  Symptoms = "Migraine", IsDone = false, CreatedAt = now,           IsDeleted = false },
        };

        public Appointment GetAppointmentById(int appointmentId)
        {
            return _appointments.FirstOrDefault(a => a.AppointmentId == appointmentId && !a.IsDeleted)!;
        }

        public Appointment AddNewAppointment(Appointment newAppointment)
        {
            int newId = _appointments.Count == 0 ? 1 : _appointments.Max(a => a.AppointmentId) + 1;

            newAppointment.AppointmentId = newId;
            newAppointment.CreatedAt = DateTimeOffset.UtcNow;
            newAppointment.IsDeleted = false;
            
            _appointments.Add(newAppointment);
            return newAppointment;
        }

        public void UpdateAppointment(Appointment appointment, int appointmentId)
        {
            var toUpdate = _appointments.FirstOrDefault(a => a.AppointmentId == appointmentId && !a.IsDeleted)!;

            toUpdate.DoctorId = appointment.DoctorId;
            toUpdate.PatientId = appointment.PatientId;
            toUpdate.Date = appointment.Date;
            toUpdate.Symptoms = appointment.Symptoms;
            toUpdate.Diagnostic = appointment.Diagnostic ?? toUpdate.Diagnostic;
            toUpdate.Medicine = appointment.Medicine ?? toUpdate.Medicine;
            toUpdate.IsDone = appointment.IsDone;
        }

        public bool DeleteAppointmentById(int appointmentId)
        {
            var toDelete = _appointments.FirstOrDefault(a => a.AppointmentId == appointmentId && !a.IsDeleted)!;
            toDelete.IsDeleted = true;
            return true;
        }

        public int GetAppointmentCount()
        {
            return _appointments.Count(a => !a.IsDeleted);
        }

        public List<Appointment> GetAppointmentPage(int page, int pageSize)
        {
            return _appointments.Where(a => !a.IsDeleted)
                .OrderBy(a => a.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public List<Appointment> GetAppointmentByDoctorId(int doctorId)
        {
            return _appointments
                .Where(a => !a.IsDeleted && a.DoctorId == doctorId).ToList();

        }

        public List<Appointment> GetAppointmentByPatientId(int patientId)
        {
            return _appointments.Where(a => !a.IsDeleted && a.PatientId == patientId).ToList();
        }

        public List<Appointment> GetAppointmentByDate(DateTimeOffset from, DateTimeOffset to)
        {
            return _appointments
                .Where(a => !a.IsDeleted && a.Date >= from && a.Date <= to).ToList();
        }

        public bool HasAppointmentConflict(Appointment appointment)
        {
            return _appointments.Any(a => !a.IsDeleted && !a.IsDone 
                                          && (a.DoctorId == appointment.DoctorId || a.PatientId == appointment.PatientId)
                                          && a.Date == appointment.Date);
        }
    }
}