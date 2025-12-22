using ClinicAPI.Query;
using ClinicAPI.Requests;
using ClinicAPI.Responses;
using ClinicAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientsController(IPatientService _patientService) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll([FromQuery] PatientSearchRequest query)
        {
            var patients = _patientService.GetPatientPage(query.Page, query.PageSize, query.IncludeAppointments);
            return Ok(patients);
        }

        [HttpGet("{patientId:int}")]
        public IActionResult GetById([FromRoute] int patientId)
        {
            try
            {
                var patient = _patientService.GetPatientById(patientId);
                return Ok(patient);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{patientId:int}/appointments")]
        public IActionResult GetAppointments([FromRoute] int patientId)
        {
            try
            {
                var appointments = _patientService.GetAppointmentByPatientId(patientId);
                return Ok(appointments);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public IActionResult AddPatient([FromBody] CreatePatientRequest createRequest)
        {
            try
            {
                var patient = _patientService.AddNewPatient(createRequest);
                return Ok(patient);
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

        [HttpPut("{id:int}")]
        public IActionResult UpdatePatient([FromBody] UpdatePatientRequest updateRequest, [FromRoute(Name = "id")] int patientId)
        {
            try
            {
                _patientService.UpdatePatient(updateRequest, patientId);
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

        [HttpDelete("{patientId:int}")]
        public IActionResult DeletePatientById([FromRoute] int patientId)
        {
            try
            {
                var patient = _patientService.DeletePatient(patientId);
                return Ok(patient);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
