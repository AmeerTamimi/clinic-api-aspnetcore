using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClinicAPI.Requests;
using ClinicAPI.Service;
using ClinicAPI.Responses;

namespace ClinicAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    // we Inject Patient Service here (We use it for business logic , so controllers dont get fat)
    public class PatientsController(IPatientService _patientService) : ControllerBase
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

        // Route Parameter Endpoint

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute(Name="id")] int patientId)
        {
            return Ok($"Patient #{patientId}");
        }

        // Query String Endpoint
        [HttpGet("search")]
        public IActionResult GetPatients([FromQuery] PatientSearchRequest request)
        {
            return Ok(request);
        }

        // Adding Patient Endpoint
        [HttpPost]
        public IActionResult AddPatient([FromBody] CreatePatientRequest NewPatient)
        {
            PatientResponse Patient = _patientService.AddNewPatient(NewPatient);
            return Ok(Patient); // result will be as a json
        }

        // Updating Patient Endpoint
        [HttpPut("{id:int}")]
        public IActionResult UpdatePatient([FromBody] UpdatePatientRequest UpdatedPatient ,[FromRoute(Name="id")] int PatientId)
        {
            try
            {
                PatientResponse Patient = _patientService.UpdatePatient(UpdatedPatient, PatientId);
                return Ok(Patient);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}