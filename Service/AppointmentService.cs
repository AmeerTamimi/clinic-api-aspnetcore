using ClinicAPI.Requests;
using ClinicAPI.Repositories;
using ClinicAPI.Responses;
using ClinicAPI.Models;

namespace ClinicAPI.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepo _appointmentRepo;

        public AppointmentService(IAppointmentRepo appointmentRepo)
        {
            _appointmentRepo = appointmentRepo;
        }

        public AppointmentResponse AddNewAppointment(CreateAppointmentRequest NewAppointment)
        {
            Appointment appointment = CreateAppointment(NewAppointment);

            _appointmentRepo.AddNewAppointment(appointment);

            AppointmentResponse appointmentResponse = CreateAppointmentResponse(appointment);
            return appointmentResponse;
        }

        public AppointmentResponse UpdateAppointment(UpdateAppointmentRequest UpdatedAppointment, int AppointmentId)
        {
            if (AppointmentId <= 0) throw new ArgumentOutOfRangeException("Appointment Id Must be > 0");
            if (UpdatedAppointment == null) throw new ArgumentNullException(nameof(UpdatedAppointment));

            Appointment CurrentAppointment = _appointmentRepo.GetAppointmentById(AppointmentId);

            if (CurrentAppointment == null) throw new KeyNotFoundException("Appointment Doesnt Exists !");

            // After More Business Validation...

            CurrentAppointment.AppointmentId = AppointmentId;
            CurrentAppointment.PatientId = UpdatedAppointment.PatientId;
            CurrentAppointment.DoctorId = UpdatedAppointment.DoctorId;
            CurrentAppointment.Date = new DateTimeOffset(UpdatedAppointment.Date.ToDateTime(TimeOnly.MinValue));

            _appointmentRepo.UpdateAppointment(CurrentAppointment, AppointmentId);

            AppointmentResponse appointmentResponse = CreateAppointmentResponse(CurrentAppointment);
            return appointmentResponse;
        }

        private Appointment CreateAppointment(CreateAppointmentRequest appointment)
        {
            return new Appointment
            {
                AppointmentId = 1,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                IsDone = false,
                Date = new DateTimeOffset(appointment.Date.ToDateTime(TimeOnly.MinValue)),
                CreatedAt = DateTimeOffset.Now,
                IsDeleted = false
            };
        }

        private AppointmentResponse CreateAppointmentResponse(Appointment appointment)
        {
            return new AppointmentResponse
            {
                AppointmentId = appointment.AppointmentId,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                Date = DateOnly.FromDateTime(appointment.Date.DateTime)
            };
        }
    }
}
