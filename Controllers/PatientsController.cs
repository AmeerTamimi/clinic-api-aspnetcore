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
            var patient = _patientService.GetPatientById(patientId);

            return Ok(patient);
        }

        [HttpGet("{patientId:int}/appointments")]
        public IActionResult GetAppointments([FromRoute] int patientId)
        {
            var appointments = _patientService.GetAppointmentByPatientId(patientId);
            
            return Ok(appointments);
        }

        [HttpPost]
        public IActionResult AddPatient([FromBody] CreatePatientRequest createRequest)
        {
            var patient = _patientService.AddNewPatient(createRequest);
            
            return Created($"Patient With Id {patient.PatientId} was Created",patient);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdatePatient([FromBody] UpdatePatientRequest updateRequest, [FromRoute(Name = "id")] int patientId)
        {
            _patientService.UpdatePatient(updateRequest, patientId);

            return NoContent();
        }

        [HttpDelete("{patientId:int}")]
        public IActionResult DeletePatientById([FromRoute] int patientId)
        {
            var patient = _patientService.DeletePatient(patientId);

            return Ok(patient);
        }
    }
}