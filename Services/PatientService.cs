using ClinicManagmentAPIs.Data;
using ClinicManagmentAPIs.DTOs;
using ClinicManagmentAPIs.Model;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagmentAPIs.Services;

public class PatientService : IPatientService
{
    private readonly DBContext _context;

    public PatientService(DBContext context)
    {
        _context = context;
    }

    public async Task<Patient> CreateAsync(CreatePatientRequest request, CancellationToken cancellationToken = default)
    {
        var patient = new Patient
        {
            Mrn = await GenerateMrnAsync(cancellationToken),
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Sex = request.Sex,
            Phone = request.Phone,
            Email = request.Email,
            Address = request.Address,
            CreatedAt = DateTime.UtcNow
        };

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync(cancellationToken);
        return patient;
    }

    public Task<Patient?> GetByIdAsync(int patientId, CancellationToken cancellationToken = default) =>
        _context.Patients
            .AsNoTracking()
            .Include(p => p.Registrations)
            .Include(p => p.Visits)
            .FirstOrDefaultAsync(p => p.PatientId == patientId, cancellationToken);

    public async Task<IReadOnlyList<Patient>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _context.Patients.AsNoTracking().OrderBy(p => p.LastName).ToListAsync(cancellationToken);

    public async Task<PatientRegistration> RegisterAsync(
        int patientId,
        RegisterPatientRequest request,
        CancellationToken cancellationToken = default)
    {
        var exists = await _context.Patients.AnyAsync(p => p.PatientId == patientId, cancellationToken);
        if (!exists)
            throw new KeyNotFoundException($"Patient {patientId} was not found.");

        var registration = new PatientRegistration
        {
            PatientId = patientId,
            RegistrationDate = DateOnly.FromDateTime(DateTime.UtcNow),
            RegistrationStatus = request.RegistrationStatus,
            VerifiedByStaffUser = request.VerifiedByStaffUser,
            Notes = request.Notes
        };

        _context.PatientRegistrations.Add(registration);
        await _context.SaveChangesAsync(cancellationToken);
        return registration;
    }

    private async Task<string> GenerateMrnAsync(CancellationToken cancellationToken)
    {
        var prefix = $"MRN-{DateTime.UtcNow:yyyyMMdd}";
        var count = await _context.Patients.CountAsync(
            p => p.Mrn.StartsWith(prefix),
            cancellationToken);
        return $"{prefix}-{(count + 1):D4}";
    }
}
