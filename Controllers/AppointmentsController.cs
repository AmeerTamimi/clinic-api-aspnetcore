using ClinicAPI.Requests;
using ClinicAPI.Responses;
using ClinicAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppointmentsController(IAppointmentService _appointmentService) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 3)
        {
            var appointments = _appointmentService.GetAppointmentPage(page, pageSize);
            return Ok(appointments);
        }

        [HttpGet("{appointmentId:int}")]
        public IActionResult GetById([FromRoute] int appointmentId)
        {
            try
            {
                var appointment = _appointmentService.GetAppointmentById(appointmentId);
                return Ok(appointment);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public IActionResult AddAppointment([FromBody] CreateAppointmentRequest createRequest)
        {
            try
            {
                var appointment = _appointmentService.AddNewAppointment(createRequest);
                return Ok(appointment);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateAppointment([FromBody] UpdateAppointmentRequest updateRequest, [FromRoute(Name = "id")] int appointmentId)
        {
            try
            {
                _appointmentService.UpdateAppointment(updateRequest, appointmentId);
                return NoContent();
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{appointmentId:int}")]
        public IActionResult DeleteAppointmentById([FromRoute] int appointmentId)
        {
            try
            {
                var appointment = _appointmentService.DeleteAppointmentById(appointmentId);
                return Ok(appointment);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
