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
        public async Task<IActionResult> GetAll([FromQuery] PatientQuery query , CancellationToken ct = default)
        {
            var patients = await _patientService.GetPatientPageAsync(query, ct);
            return Ok(patients);
        }

        [HttpGet("{patientId:int}")]
        public async Task<IActionResult> GetById(int patientId, [FromQuery] PatientQuery query, CancellationToken ct = default)
        {
            var patient = await _patientService.GetPatientByIdAsync(patientId, query, ct);
            return Ok(patient);
        }

        [HttpGet("{patientId:int}/appointments")]
        public async Task<IActionResult> GetAppointments(int patientId, [FromQuery] AppointmentQuery query, CancellationToken ct = default)
        {
            var appointments = await _patientService.GetAppointmentByPatientIdAsync(patientId, query, ct);
            return Ok(appointments);
        }

        [HttpPost]
        public async Task<IActionResult> AddPatient([FromBody] CreatePatientRequest createRequest, CancellationToken ct = default)
        {
            var patient = await _patientService.AddNewPatientAsync(createRequest, ct);
            return Created($"Patient With Id {patient.PatientId} was Created", patient);
        }

        [HttpPut("{patientId:int}")]
        public async Task<IActionResult> UpdatePatient([FromBody] UpdatePatientRequest updateRequest,int patientId, CancellationToken ct = default)
        {
            await _patientService.UpdatePatientAsync(updateRequest, patientId, ct);
            return NoContent();
        }

        [HttpDelete("{patientId:int}")]
        public async Task<IActionResult> DeletePatientById([FromRoute] int patientId, CancellationToken ct = default)
        {
            var patient = await _patientService.DeletePatientAsync(patientId, ct);
            return Ok(patient);
        }
    }
}
