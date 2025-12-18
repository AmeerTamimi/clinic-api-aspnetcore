using Microsoft.AspNetCore.Mvc;
using ClinicAPI.Requests;
using ClinicAPI.Service;
using ClinicAPI.Responses;
using ClinicAPI.Query;

namespace ClinicAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppointmentsController(IAppointmentService _appointmentService) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new[]
            {
                "Appointment #1",
                "Appointment #2"
            });
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute(Name = "id")] int appointmentId)
        {
            return Ok($"Appointment #{appointmentId}");
        }

        [HttpGet("search")]
        public IActionResult GetAppointments([FromQuery] AppointmentSearchRequest request)
        {
            return Ok(request);
        }

        [HttpPost]
        public IActionResult AddAppointment([FromBody] CreateAppointmentRequest NewAppointment)
        {
            try
            {
                AppointmentResponse Appointment = _appointmentService.AddNewAppointment(NewAppointment);
                return Ok(Appointment);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateAppointment([FromBody] UpdateAppointmentRequest UpdatedAppointment, [FromRoute(Name = "id")] int AppointmentId)
        {
            try
            {
                AppointmentResponse Appointment = _appointmentService.UpdateAppointment(UpdatedAppointment, AppointmentId);
                return Ok(Appointment);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}
