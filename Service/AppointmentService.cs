using ClinicAPI.CustomExceptions;
using ClinicAPI.Models;
using ClinicAPI.Repositories;
using ClinicAPI.Requests;
using ClinicAPI.Responses;

namespace ClinicAPI.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepo _appointmentRepo;
        private readonly IPatientRepo _patientRepo;
        private readonly IDoctorRepo _doctorRepo;
        public AppointmentService(IAppointmentRepo appointmentRepo , IPatientRepo patientRepo , IDoctorRepo doctorRepo)
        {
            _appointmentRepo = appointmentRepo;
            _patientRepo = patientRepo;
            _doctorRepo = doctorRepo;
        }

        public AppointmentResponse GetAppointmentById(int appointmentId)
        {
            var appointment = _appointmentRepo.GetAppointmentById(appointmentId);

            if (appointment is null)
                throw new NotFoundException("Appointment Does Not Exist");

            return AppointmentResponse.FromModel(appointment);
        }

        public AppointmentResponse AddNewAppointment(CreateAppointmentRequest appointmentRequest)
        {
            IsValidAppointment(appointmentRequest);

            var appointment = FromCreateRequestToModel(appointmentRequest);

            if (_appointmentRepo.HasAppointmentConflict(appointment))
                throw new ConflictException("Appointment Conflict , Change the Date");

            var created = _appointmentRepo.AddNewAppointment(appointment);

            return AppointmentResponse.FromModel(created);
        }

        public void UpdateAppointment(UpdateAppointmentRequest appointmentRequest, int appointmentId)
        {
            IsValidAppointment(appointmentRequest, appointmentId);

            var appointment = FromUpdateRequestToModel(appointmentRequest, appointmentId);

            if (_appointmentRepo.HasAppointmentConflict(appointment))
                throw new ConflictException("Appointment Conflict , Change the Date");

            _appointmentRepo.UpdateAppointment(appointment, appointmentId);
        }

        public AppointmentResponse DeleteAppointmentById(int appointmentId)
        {
            var appointment = _appointmentRepo.GetAppointmentById(appointmentId);

            if (appointment is null)
                throw new NotFoundException("Appointment Does Not Exist");

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

        private void IsValidAppointment(CreateAppointmentRequest appointmentRequest)
        {
            if (appointmentRequest is null)
                throw new ValidationException("Invalid Appointment");

            int patientId = appointmentRequest.PatientId;
            if (_patientRepo.GetPatientById(patientId) is null)
                throw new NotFoundException($"Patient With Id {patientId} Does Not Exist");

            int doctorId = appointmentRequest.DoctorId;
            if (_doctorRepo.GetDoctorById(doctorId) is null)
                throw new NotFoundException($"Doctor With Id {doctorId} Does Not Exist");

        }

        private void IsValidAppointment(UpdateAppointmentRequest appointmentRequest, int appointmentId)
        {
            var appointment = _appointmentRepo.GetAppointmentById(appointmentId);
            if (appointment is null)
                throw new NotFoundException("Appointment Does Not Exist");

            if (appointmentRequest is null)
                throw new ValidationException("Invalid Appointment");
            

        }

        private Appointment FromCreateRequestToModel(CreateAppointmentRequest appointmentRequest)
        {
            var appointment = new Appointment
            {
                PatientId = appointmentRequest.PatientId,
                DoctorId = appointmentRequest.DoctorId,
                Date = appointmentRequest.Date,
                Symptoms = appointmentRequest.Symptoms,
                Diagnostic = appointmentRequest.Diagnostic,
                Medicine = appointmentRequest.Medicine
            };

            return appointment;
        }

        private Appointment FromUpdateRequestToModel(UpdateAppointmentRequest appointmentRequest, int appointmentId)
        {
            var appointment = new Appointment
            {
                AppointmentId = appointmentId,
                PatientId = appointmentRequest.PatientId,
                DoctorId = appointmentRequest.DoctorId,
                Date = appointmentRequest.Date,
                IsDone = appointmentRequest.IsDone,
                Symptoms = appointmentRequest.Symptoms,
                Diagnostic = appointmentRequest.Diagnostic,
                Medicine = appointmentRequest.Medicine
            };

            return appointment;
        }
    }
}
