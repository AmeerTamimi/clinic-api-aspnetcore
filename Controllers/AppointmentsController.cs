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
        public IActionResult GetAll([FromQuery] AppointmentQuery query)
        {
            var appointments = _appointmentService.GetAppointmentPage(query);

            return Ok(appointments);
        }

        [HttpGet("{appointmentId:int}")]
        public IActionResult GetById([FromRoute] int appointmentId)
        {
            var appointment = _appointmentService.GetAppointmentById(appointmentId);

            return Ok(appointment);
        }

        [HttpPost]
        public IActionResult AddAppointment([FromBody] CreateAppointmentRequest createRequest)
        {
            var appointment = _appointmentService.AddNewAppointment(createRequest);

            return Created($"Appointment With Patient Id : {appointment.PatientId} ,Doctor Id :{appointment.DoctorId} Was created "
                ,appointment);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateAppointment([FromBody] UpdateAppointmentRequest updateRequest, [FromRoute(Name = "id")] int appointmentId)
        {
            _appointmentService.UpdateAppointment(updateRequest, appointmentId);

            return NoContent();
        }

        [HttpDelete("{appointmentId:int}")]
        public IActionResult DeleteAppointmentById([FromRoute] int appointmentId)
        {
            var appointment = _appointmentService.DeleteAppointmentById(appointmentId);

            return Ok(appointment);
    }
    }
}
