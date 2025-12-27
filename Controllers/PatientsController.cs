using ClinicAPI.Query;
using ClinicAPI.Requests;
using ClinicAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientsController(IPatientService _patientService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PatientQuery query)
        {
            var patients = await _patientService.GetPatientPageAsync(query);
            return Ok(patients);
        }

        [HttpGet("{patientId:int}")]
        public async Task<IActionResult> GetById(int patientId, [FromQuery] PatientQuery query)
        {
            var patient = await _patientService.GetPatientByIdAsync(patientId, query);
            return Ok(patient);
        }

        [HttpGet("{patientId:int}/appointments")]
        public async Task<IActionResult> GetAppointments(int patientId, [FromQuery] AppointmentQuery query)
        {
            var appointments = await _patientService.GetAppointmentByPatientIdAsync(patientId, query);
            return Ok(appointments);
        }

        [HttpPost]
        public async Task<IActionResult> AddPatient([FromBody] CreatePatientRequest createRequest)
        {
            var patient = await _patientService.AddNewPatientAsync(createRequest);
            return Created($"Patient With Id {patient.PatientId} was Created", patient);
        }

        [HttpPut("{patientId:int}")]
        public async Task<IActionResult> UpdatePatient([FromBody] UpdatePatientRequest updateRequest,int patientId)
        {
            await _patientService.UpdatePatientAsync(updateRequest, patientId);
            return NoContent();
        }

        [HttpDelete("{patientId:int}")]
        public async Task<IActionResult> DeletePatientById([FromRoute] int patientId)
        {
            var patient = await _patientService.DeletePatientAsync(patientId);
            return Ok(patient);
        }
    }
}
