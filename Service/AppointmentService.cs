using ClinicAPI.Models;
using ClinicAPI.Repositories;
using ClinicAPI.Requests;
using ClinicAPI.Responses;

namespace ClinicAPI.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepo _appointmentRepo;

        public AppointmentService(IAppointmentRepo appointmentRepo)
        {
            _appointmentRepo = appointmentRepo;
        }

        public AppointmentResponse GetAppointmentById(int appointmentId)
        {
            var appointment = _appointmentRepo.GetAppointmentById(appointmentId);

            if (appointment is null)
                throw new ArgumentNullException("Appointment Does Not Exist");

            return AppointmentResponse.FromModel(appointment);
        }

        public AppointmentResponse AddNewAppointment(CreateAppointmentRequest newAppointment)
        {
            IsValidAppointment(newAppointment);

            var appointment = FromRequestToModel(newAppointment);

            var created = _appointmentRepo.AddNewAppointment(appointment);

            return AppointmentResponse.FromModel(created);
        }

        public AppointmentResponse UpdateAppointment(UpdateAppointmentRequest appointment, int appointmentId)
        {
            IsValidAppointment(appointment, appointmentId);

            var model = FromRequestToModel(appointment, appointmentId);

            var updated = _appointmentRepo.UpdateAppointment(model, appointmentId);

            return AppointmentResponse.FromModel(updated);
        }

        public AppointmentResponse DeleteAppointmentById(int appointmentId)
        {
            var appointment = _appointmentRepo.GetAppointmentById(appointmentId);

            if (appointment is null)
                throw new ArgumentNullException("Appointment Does Not Exist");

            _appointmentRepo.DeleteAppointmentById(appointmentId);

            return AppointmentResponse.FromModel(appointment);
        }

        public PagedResult<AppointmentResponse> GetAppointmentPage(int page, int pageSize)
        {
            page = Math.Max(page, 1);
            pageSize = Math.Clamp(pageSize, 3, 100);

            var items = _appointmentRepo.GetAppointmentPage(page, pageSize);

            var itemsResponse = AppointmentResponse.FromModels(items);

            var totalItems = _appointmentRepo.GetAppointmentCount();

            return PagedResult<AppointmentResponse>.GetPagedItems(itemsResponse, totalItems, page, pageSize);
        }

        private void IsValidAppointment(AppointmentRequest appointmentRequest, int id = 0)
        {
            if (id > 0)
            {
                var appointment = _appointmentRepo.GetAppointmentById(id);
                if (appointment is null)
                    throw new ArgumentNullException("Appointment Does Not Exist");
            }

            if (appointmentRequest is null)
                throw new ArgumentNullException("Invalid Appointment");

            var now = DateTimeOffset.UtcNow;
            if (now > appointmentRequest.Date)
                throw new ArgumentException("Date Must Be In The Future");
        }

        private Appointment FromRequestToModel(AppointmentRequest appointmentRequest, int appointmentId = 0)
        {
            var appointment = new Appointment
            {
                PatientId = appointmentRequest.PatientId,
                DoctorId = appointmentRequest.DoctorId,
                Date = appointmentRequest.Date
            };

            if (appointmentRequest is UpdateAppointmentRequest u)
            {
                appointment.AppointmentId = appointmentId;
                appointment.PatientId = u.PatientId;
                appointment.DoctorId = u.DoctorId;
                appointment.Date = u.Date;
                appointment.IsDone = u.IsDone;
            }

            return appointment;
        }
    }
}
