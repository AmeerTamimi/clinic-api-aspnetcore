using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
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
        public IActionResult GetById(int id)
        {
            return Ok($"Doctor #{id}");
        }

        [HttpGet("{doctorId:int}/patients")] // To Get All Patients that a specific Doctor is Dealing with them
        public IActionResult GetPatients(int doctorId)
        {
            return Ok(new[]
            {
                "Patient #1"
            });
        }

        [HttpGet("{doctorId:int}/appointments")] // To Get All Appointments that  a specific Doctor Has
        public IActionResult GetAppoitnments(int doctorId)
        {
            return Ok(new[]
            {
                "Appointment #1"
            });
        }
    }
}
