using ClinicAPI.Repositories;

namespace ClinicAPI.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepo _appointmentRepo;

        public AppointmentService(IAppointmentRepo appointmentRepo)
        {
            appointmentRepo = _appointmentRepo;
        }
    }
}
