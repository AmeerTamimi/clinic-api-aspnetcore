using ClinicAPI.Permissions;
using ClinicAPI.Query;
using ClinicAPI.Requests;
using ClinicAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientsController(IPatientService _patientService) : ControllerBase
    {
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer" , Policy = Permission.Patient.Read)] // same As [Authorize] , since we registerd the default as "Bearer"
        public async Task<IActionResult> GetAll([FromQuery] PatientQuery query , CancellationToken ct = default)
        {
            var patients = await _patientService.GetPatientPageAsync(query, ct);
            return Ok(patients);
        }

        [HttpGet("{patientId:int}")]
        [Authorize(Policy = Permission.Patient.Read)]
        public async Task<IActionResult> GetById(int patientId, [FromQuery] bool includeAppointments = false, CancellationToken ct = default)
        {
            var patient = await _patientService.GetPatientByIdAsync(patientId, includeAppointments, ct);
            return Ok(patient);
        }

        [HttpGet("{patientId:int}/appointments")]
        [Authorize(Policy = Permission.Patient.ReadAppointments)]
        public async Task<IActionResult> GetAppointments(int patientId, [FromQuery] AppointmentQuery query, CancellationToken ct = default)
        {
            var appointments = await _patientService.GetAppointmentByPatientIdAsync(patientId, query, ct);
            return Ok(appointments);
        }

        [HttpPost]
        [Authorize(Policy = Permission.Patient.Create)]
        public async Task<IActionResult> AddPatient([FromBody] CreatePatientRequest createRequest, CancellationToken ct = default)
        {
            var patient = await _patientService.AddNewPatientAsync(createRequest, ct);
            return Created($"Patient With Id {patient.UserId} was Created", patient);
        }

        [HttpPut("{patientId:int}")]
        [Authorize(Policy = Permission.Patient.Update)]
        public async Task<IActionResult> UpdatePatient([FromBody] UpdatePatientRequest updateRequest,int patientId, CancellationToken ct = default)
        {
            await _patientService.UpdatePatientAsync(updateRequest, patientId, ct);
            return NoContent();
        }

        [HttpDelete("{patientId:int}")]
        [Authorize(Policy = Permission.Patient.Delete)]
        public async Task<IActionResult> DeletePatientById([FromRoute] int patientId, CancellationToken ct = default)
        {
            var patient = await _patientService.DeletePatientAsync(patientId, ct);
            return Ok(patient);
        }
    }
}
