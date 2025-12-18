using ClinicAPI.Models;
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
        public IActionResult GetAll()
        {
            return Ok(new[]
            {
                "Doctor #1",
                "Doctor #2"
            });
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute(Name = "id")] int doctorId)
        {
            return Ok($"Doctor #{doctorId}");
        }

        [HttpGet("{doctorId:int}/patients")]
        public IActionResult GetPatients([FromRoute(Name = "doctorId")] int doctorId)
        {
            return Ok(new[]
            {
                "Patient #1"
            });
        }

        [HttpGet("{doctorId:int}/appointments")]
        public IActionResult GetAppointments([FromRoute(Name = "doctorId")] int doctorId)
        {
            return Ok(new[]
            {
                "Appointment #1"
            });
        }

        [HttpGet("search")]
        public IActionResult GetDoctors([FromQuery] DoctorSearchRequest request)
        {
            return Ok(request);
        }

        [HttpPost]
        public IActionResult AddDoctor([FromBody] CreateDoctorRequest NewDoctor)
        {
            try
            {
                DoctorResponse Doctor = _doctorService.AddNewDoctor(NewDoctor);
                return Ok(Doctor);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateDoctor([FromBody] UpdateDoctorRequest UpdatedDoctor, [FromRoute(Name = "id")] int DoctorId)
        {
            try
            {
                DoctorResponse Doctor = _doctorService.UpdateDoctor(UpdatedDoctor, DoctorId);
                return Ok(Doctor);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}
