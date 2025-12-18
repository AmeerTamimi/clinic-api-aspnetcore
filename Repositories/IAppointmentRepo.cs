using ClinicAPI.Models;

namespace ClinicAPI.Repositories
{
    public interface IAppointmentRepo
    {
        Appointment? GetAppointmentById(int appointmentId);
        Appointment AddNewAppointment(Appointment newAppointment);
        Appointment UpdateAppointment(Appointment appointment , int appointmentId);
        bool DeleteAppointmentById(int appointmentId);
        int GetAppointmentCount();
        List<Appointment> GetAppointmentByDate(DateTimeOffset from, DateTimeOffset to);
        List<Appointment> GetAppointmentPage(int page, int pageSize);
        List<Appointment> GetAppointmentByPatientId(int patientId);
        List<Appointment> GetAppointmentByDoctorId(int doctorId);
    }
}
