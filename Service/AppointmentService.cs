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
            throw new NotImplementedException();
        }

        public AppointmentResponse UpdateAppointment(UpdateAppointmentRequest Appointment, int AppointmentId)
        {
            throw new NotImplementedException();
        }
    }
}
