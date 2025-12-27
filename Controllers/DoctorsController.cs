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
            var doctor = _doctorService.GetDoctorById(doctorId);

            return Ok(doctor);
        }

        [HttpGet("{doctorId:int}/patients")]
        public IActionResult GetPatients([FromRoute] int doctorId, [FromQuery] bool includeAppointments = false)
        {

            var patients = _doctorService.GetDoctorPatients(doctorId, includeAppointments);
            
            return Ok(patients);
        }

        [HttpGet("{doctorId:int}/appointments")]
        public IActionResult GetAppointments([FromRoute] int doctorId)
        {
            var appointments = _doctorService.GetDoctorAppointments(doctorId);
            
            return Ok(appointments);
        }

        [HttpPost]
        public IActionResult AddDoctor([FromBody] CreateDoctorRequest createRequest)
        {
            var doctor = _doctorService.AddNewDoctor(createRequest);
                
            return Created($"Doctor With Id {doctor.DoctorId} Was Created",doctor);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateDoctor([FromBody] UpdateDoctorRequest updateRequest, [FromRoute(Name = "id")] int doctorId)
        {
            _doctorService.UpdateDoctor(updateRequest, doctorId);

            return NoContent();
        }

        [HttpDelete("{doctorId:int}")]
        public IActionResult DeleteDoctorById([FromRoute] int doctorId)
        {
            var doctor = _doctorService.DeleteDoctorById(doctorId);

            return Ok(doctor);
        }
    }
}