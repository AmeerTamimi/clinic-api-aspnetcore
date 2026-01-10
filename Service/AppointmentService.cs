using ClinicAPI.CustomExceptions;
using ClinicAPI.Models;
using ClinicAPI.Query;
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

        public AppointmentService(IAppointmentRepo appointmentRepo, IPatientRepo patientRepo, IDoctorRepo doctorRepo)
        {
            _appointmentRepo = appointmentRepo;
            _patientRepo = patientRepo;
            _doctorRepo = doctorRepo;
        }

        public async Task<AppointmentResponse> GetAppointmentByIdAsync(int appointmentId, CancellationToken ct = default)
        {
            var appointment = await _appointmentRepo.GetAppointmentByIdAsync(appointmentId, ct);

            if (appointment is null)
                throw new NotFoundException("Appointment Does Not Exist");

            return AppointmentResponse.FromModel(appointment);
        }

        public async Task<AppointmentResponse> AddNewAppointmentAsync(CreateAppointmentRequest appointmentRequest, CancellationToken ct = default)
        {
            await IsValidAppointmentAsync(appointmentRequest, ct);

            var appointment = FromCreateRequestToModel(appointmentRequest);

            if (await _appointmentRepo.HasAppointmentConflictAsync(appointment, ct))
                throw new ConflictException("Appointment Conflict , Change the Date");

            var created = await _appointmentRepo.AddNewAppointmentAsync(appointment, ct);

            return AppointmentResponse.FromModel(created);
        }

        public async Task UpdateAppointmentAsync(UpdateAppointmentRequest appointmentRequest, int appointmentId, CancellationToken ct = default)
        {
            await IsValidAppointmentAsync(appointmentRequest, appointmentId, ct);

            var appointment = FromUpdateRequestToModel(appointmentRequest, appointmentId);

            if (await _appointmentRepo.HasAppointmentConflictAsync(appointment, ct))
                throw new ConflictException("Appointment Conflict , Change the Date");
        }

        public async Task<AppointmentResponse> DeleteAppointmentByIdAsync(int appointmentId, CancellationToken ct = default)
        {
            var appointment = await _appointmentRepo.GetAppointmentByIdAsync(appointmentId, ct);

            if (appointment is null)
                throw new NotFoundException("Appointment Does Not Exist");

            var succeded = await _appointmentRepo.DeleteAppointmentByIdAsync(appointmentId, ct);

            if (!succeded)
                throw new ServerException("Sorry, couldn't delete the Appointment");

            return AppointmentResponse.FromModel(appointment);
        }

        public async Task<PagedResult<AppointmentResponse>> GetAppointmentPageAsync(AppointmentQuery query, CancellationToken ct = default)
        {
            int page = query.Page;
            int pageSize = query.PageSize;

            page = Math.Max(page, 1);
            pageSize = Math.Clamp(pageSize, 3, 100);

            var items = await _appointmentRepo.GetAppointmentPageAsync(page, pageSize, ct);

            var itemsResponse = AppointmentResponse.FromModels(items, query);

            var totalItems = await _appointmentRepo.GetAppointmentCountAsync(ct);

            return PagedResult<AppointmentResponse>.GetPagedItems(itemsResponse, totalItems, page, pageSize);
        }

        private async Task IsValidAppointmentAsync(CreateAppointmentRequest appointmentRequest, CancellationToken ct = default)
        {
            if (appointmentRequest is null)
                throw new ValidationException("Invalid Appointment");

            int patientId = appointmentRequest.PatientId;
            if (await _patientRepo.GetPatientByIdAsync(patientId, ct) is null)
                throw new NotFoundException($"Patient With Id {patientId} Does Not Exist");

            int doctorId = appointmentRequest.DoctorId;
            if (await _doctorRepo.GetDoctorByIdAsync(doctorId, ct) is null)
                throw new NotFoundException($"Doctor With Id {doctorId} Does Not Exist");
        }

        private async Task IsValidAppointmentAsync(UpdateAppointmentRequest appointmentRequest, int appointmentId, CancellationToken ct = default)
        {
            var appointment = await _appointmentRepo.GetAppointmentByIdAsync(appointmentId, ct);
            if (appointment is null)
                throw new NotFoundException("Appointment Does Not Exist");

            if (appointmentRequest is null)
                throw new ValidationException("Invalid Appointment");
        }

        private Appointment FromCreateRequestToModel(CreateAppointmentRequest appointmentRequest)
        {
            var appointment = new Appointment
            {
                PatientUserId = appointmentRequest.PatientId,
                DoctorUserId = appointmentRequest.DoctorId,
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
                PatientUserId = appointmentRequest.PatientId,
                DoctorUserId = appointmentRequest.DoctorId,
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
