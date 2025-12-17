using Microsoft.AspNetCore.Mvc;
using ClinicAPI.Requests;
using ClinicAPI.Service;
using ClinicAPI.Responses;

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
            DoctorResponse Doctor = _doctorService.AddNewDoctor(NewDoctor);
            return Ok(Doctor);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateDoctor([FromBody] UpdateDoctorRequest UpdatedDoctor, [FromRoute(Name = "id")] int DoctorId)
        {
            DoctorResponse Doctor = _doctorService.UpdateDoctor(UpdatedDoctor, DoctorId);
            return Ok(Doctor);
        }
    }
}
