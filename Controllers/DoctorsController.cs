using ClinicAPI.Query;
using ClinicAPI.Requests;
using ClinicAPI.Responses;
using ClinicAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DoctorsController(IDoctorService _doctorService) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll([FromQuery] DoctorSearchRequest query)
        {
            var doctors = _doctorService.GetDoctorPage(query);
            return Ok(doctors);
        }

        [HttpGet("{doctorId:int}")]
        public IActionResult GetById([FromRoute] int doctorId)
        {
            try
            {
                var doctor = _doctorService.GetDoctorById(doctorId);
                return Ok(doctor);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{doctorId:int}/patients")]
        public IActionResult GetPatients([FromRoute] int doctorId, [FromQuery] bool includeAppointments = false)
        {
            try
            {
                var patients = _doctorService.GetDoctorPatients(doctorId, includeAppointments);
                return Ok(patients);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{doctorId:int}/appointments")]
        public IActionResult GetAppointments([FromRoute] int doctorId)
        {
            try
            {
                var appointments = _doctorService.GetDoctorAppointments(doctorId);
                return Ok(appointments);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public IActionResult AddDoctor([FromBody] CreateDoctorRequest createRequest)
        {
            try
            {
                var doctor = _doctorService.AddNewDoctor(createRequest);
                return Ok(doctor);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateDoctor([FromBody] UpdateDoctorRequest updateRequest, [FromRoute(Name = "id")] int doctorId)
        {
            try
            {
                _doctorService.UpdateDoctor(updateRequest, doctorId);
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

        [HttpDelete("{doctorId:int}")]
        public IActionResult DeleteDoctorById([FromRoute] int doctorId)
        {
            try
            {
                var doctor = _doctorService.DeleteDoctorById(doctorId);
                return Ok(doctor);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
