using ClinicAPI.Models;
using ClinicAPI.Presistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicAPI.Repositories
{
    public class AppointmentRepo(ClinicDbContext context) : IAppointmentRepo
    {
        


        public Appointment GetAppointmentById(int appointmentId)
        {
            return context.Appointments.FirstOrDefault(a => a.AppointmentId == appointmentId && !a.IsDeleted)!;
        }

        public Appointment AddNewAppointment(Appointment newAppointment)
        {
            int newId = context.Appointments.Count() == 0 ? 1 : context.Appointments.Max(a => a.AppointmentId) + 1;

            newAppointment.AppointmentId = newId;
            newAppointment.CreatedAt = DateTimeOffset.UtcNow;
            newAppointment.IsDeleted = false;

            context.Appointments.Add(newAppointment);

            context.SaveChanges();
            return newAppointment;
        }

        public void UpdateAppointment(Appointment appointment, int appointmentId)
        {
            var toUpdate = context.Appointments.FirstOrDefault(a => a.AppointmentId == appointmentId && !a.IsDeleted)!;

            toUpdate.DoctorId = appointment.DoctorId;
            toUpdate.PatientId = appointment.PatientId;
            toUpdate.Date = appointment.Date;
            toUpdate.Symptoms = appointment.Symptoms;
            toUpdate.Diagnostic = appointment.Diagnostic ?? toUpdate.Diagnostic;
            toUpdate.Medicine = appointment.Medicine ?? toUpdate.Medicine;
            toUpdate.IsDone = appointment.IsDone;

            context.SaveChanges();
        }

        public bool DeleteAppointmentById(int appointmentId)
        {
            var toDelete = context.Appointments.FirstOrDefault(a => a.AppointmentId == appointmentId && !a.IsDeleted)!;
            toDelete.IsDeleted = true;
            context.SaveChanges();
            return true;
        }

        public int GetAppointmentCount()
        {
            return context.Appointments.Count(a => !a.IsDeleted);
        }

        public List<Appointment> GetAppointmentPage(int page, int pageSize)
        {
            return context.Appointments.Where(a => !a.IsDeleted)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public List<Appointment> GetAppointmentByDoctorId(int doctorId)
        {
            return context.Appointments
                .Where(a => !a.IsDeleted && a.DoctorId == doctorId).ToList();

        }

        public List<Appointment> GetAppointmentByPatientId(int patientId)
        {
            return context.Appointments.Where(a => !a.IsDeleted && a.PatientId == patientId).ToList();
        }

        public List<Appointment> GetAppointmentByDate(DateTimeOffset from, DateTimeOffset to)
        {
            return context.Appointments
                .Where(a => !a.IsDeleted && a.Date >= from && a.Date <= to).ToList();
        }

        public bool HasAppointmentConflict(Appointment appointment)
        {
            return context.Appointments.Any(a => !a.IsDeleted && !a.IsDone 
                                          && (a.DoctorId == appointment.DoctorId || a.PatientId == appointment.PatientId)
                                          && a.Date == appointment.Date);
        }
    }
}