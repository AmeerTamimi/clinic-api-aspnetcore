using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new[] // Dummy (Only For Now i swear)
            {
                "Patient #1" ,
                "Patient #2"
            });
        }


        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok($"Patient #{id}");
        }

    }
}