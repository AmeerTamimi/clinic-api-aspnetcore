using ClinicAPI.Query;
using ClinicAPI.Requests;
using ClinicAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DoctorsController(IDoctorService _doctorService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] DoctorQuery query, CancellationToken ct)
        {
            var doctors = await _doctorService.GetDoctorPageAsync(query, ct);
            return Ok(doctors);
        }

        [HttpGet("{doctorId:int}")]
        public async Task<IActionResult> GetById([FromRoute] int doctorId, [FromQuery] DoctorQuery query, CancellationToken ct)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(doctorId, query, ct);
            return Ok(doctor);
        }

        [HttpGet("{doctorId:int}/patients")]
        public async Task<IActionResult> GetPatients([FromRoute] int doctorId, [FromQuery] PatientQuery query, CancellationToken ct)
        {
            var patients = await _doctorService.GetDoctorPatientsAsync(doctorId, query, ct);
            return Ok(patients);
        }

        [HttpGet("{doctorId:int}/appointments")]
        public async Task<IActionResult> GetAppointments([FromRoute] int doctorId, [FromQuery] AppointmentQuery query, CancellationToken ct)
        {
            var appointments = await _doctorService.GetDoctorAppointmentsAsync(doctorId, query, ct);
            return Ok(appointments);
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctor([FromBody] CreateDoctorRequest createRequest, CancellationToken ct)
        {
            var doctor = await _doctorService.AddNewDoctorAsync(createRequest, ct);
            return Created($"Doctor With Id {doctor.DoctorId} Was Created", doctor);
        }

        [HttpPut("{doctorId:int}")]
        public async Task<IActionResult> UpdateDoctor([FromBody] UpdateDoctorRequest updateRequest, int doctorId, CancellationToken ct)
        {
            await _doctorService.UpdateDoctorAsync(updateRequest, doctorId, ct);
            return NoContent();
        }

        [HttpDelete("{doctorId:int}")]
        public async Task<IActionResult> DeleteDoctorById([FromRoute] int doctorId, CancellationToken ct)
        {
            var doctor = await _doctorService.DeleteDoctorByIdAsync(doctorId, ct);
            return Ok(doctor);
        }
    }
}
