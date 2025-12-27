using ClinicAPI.Query;
using ClinicAPI.Requests;
using ClinicAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppointmentsController(IAppointmentService _appointmentService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AppointmentQuery query)
        {
            var appointments = await _appointmentService.GetAppointmentPageAsync(query);
            return Ok(appointments);
        }

        [HttpGet("{appointmentId:int}")]
        public async Task<IActionResult> GetById([FromRoute] int appointmentId)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(appointmentId);
            return Ok(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> AddAppointment([FromBody] CreateAppointmentRequest createRequest)
        {
            var appointment = await _appointmentService.AddNewAppointmentAsync(createRequest);

            return Created(
                $"Appointment With Patient Id : {appointment.PatientId} ,Doctor Id :{appointment.DoctorId} Was created ",
                appointment
            );
        }

        [HttpPut("{appointmentId:int}")]
        public async Task<IActionResult> UpdateAppointment([FromBody] UpdateAppointmentRequest updateRequest, int appointmentId)
        {
            await _appointmentService.UpdateAppointmentAsync(updateRequest, appointmentId);
            return NoContent();
        }

        [HttpDelete("{appointmentId:int}")]
        public async Task<IActionResult> DeleteAppointmentById([FromRoute] int appointmentId)
        {
            var appointment = await _appointmentService.DeleteAppointmentByIdAsync(appointmentId);
            return Ok(appointment);
        }
    }
}
