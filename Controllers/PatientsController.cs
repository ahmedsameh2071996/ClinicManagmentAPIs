using ClinicManagmentAPIs.DTOs;
using ClinicManagmentAPIs.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagmentAPIs.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var patients = await _patientService.GetAllAsync(cancellationToken);
        return Ok(patients);
    }

    [HttpGet("{patientId:int}")]
    public async Task<IActionResult> GetById(int patientId, CancellationToken cancellationToken)
    {
        var patient = await _patientService.GetByIdAsync(patientId, cancellationToken);
        return patient is null ? NotFound() : Ok(patient);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePatientRequest request, CancellationToken cancellationToken)
    {
        var patient = await _patientService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { patientId = patient.PatientId }, patient);
    }

    [HttpPost("{patientId:int}/register")]
    public async Task<IActionResult> Register(
        int patientId,
        [FromBody] RegisterPatientRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var registration = await _patientService.RegisterAsync(patientId, request, cancellationToken);
            return Ok(registration);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
