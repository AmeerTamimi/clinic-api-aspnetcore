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
        public IActionResult GetAll([FromQuery] DoctorQuery query)
        {
            var doctors = _doctorService.GetDoctorPage(query);

            return Ok(doctors);
        }

        [HttpGet("{doctorId:int}")]
        public IActionResult GetById([FromRoute] int doctorId , [FromQuery] DoctorQuery query)
        {
            var doctor = _doctorService.GetDoctorById(doctorId , query);

            return Ok(doctor);
        }

        [HttpGet("{doctorId:int}/patients")]
        public IActionResult GetPatients([FromRoute] int doctorId, [FromQuery] PatientQuery query)
        {

            var patients = _doctorService.GetDoctorPatients(doctorId, query);
            
            return Ok(patients);
        }

        [HttpGet("{doctorId:int}/appointments")]
        public IActionResult GetAppointments([FromRoute] int doctorId , [FromQuery] AppointmentQuery query)
        {
            var appointments = _doctorService.GetDoctorAppointments(doctorId , query);
            
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