using ClinicAPI.Permissions;
using ClinicAPI.Query;
using ClinicAPI.Requests;
using ClinicAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppointmentsController(IAppointmentService _appointmentService) : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = Permission.Appointment.Read)]
        public async Task<IActionResult> GetAll([FromQuery] AppointmentQuery query, CancellationToken ct)
        {
            var appointments = await _appointmentService.GetAppointmentPageAsync(query, ct);
            return Ok(appointments);
        }

        [HttpGet("{appointmentId:int}")]
        [Authorize(Policy = Permission.Appointment.Read)]
        public async Task<IActionResult> GetById([FromRoute] int appointmentId, CancellationToken ct)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(appointmentId, ct);
            return Ok(appointment);
        }

        [HttpPost]
        [Authorize(Policy = Permission.Appointment.Create)]
        public async Task<IActionResult> AddAppointment([FromBody] CreateAppointmentRequest createRequest, CancellationToken ct)
        {
            var appointment = await _appointmentService.AddNewAppointmentAsync(createRequest, ct);

            return Created(
                $"Appointment With Patient Id : {appointment.PatientId} ,Doctor Id :{appointment.DoctorId} Was created ",
                appointment
            );
        }

        [HttpPut("{appointmentId:int}")]
        [Authorize(Policy = Permission.Appointment.Update)]
        public async Task<IActionResult> UpdateAppointment([FromBody] UpdateAppointmentRequest updateRequest, int appointmentId, CancellationToken ct)
        {
            await _appointmentService.UpdateAppointmentAsync(updateRequest, appointmentId, ct);
            return NoContent();
        }

        [HttpDelete("{appointmentId:int}")]
        [Authorize(Policy = Permission.Appointment.Delete)]
        public async Task<IActionResult> DeleteAppointmentById([FromRoute] int appointmentId, CancellationToken ct)
        {
            var appointment = await _appointmentService.DeleteAppointmentByIdAsync(appointmentId, ct);
            return Ok(appointment);
        }
    }
}
