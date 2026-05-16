using ClinicManagmentAPIs.Data;
using ClinicManagmentAPIs.DTOs;
using ClinicManagmentAPIs.Model;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagmentAPIs.Services;

public class VisitService : IVisitService
{
    private readonly DBContext _context;

    public VisitService(DBContext context)
    {
        _context = context;
    }

    public async Task<Visit> CreateForPatientAsync(
        int patientId,
        CreateVisitRequest request,
        CancellationToken cancellationToken = default)
    {
        var exists = await _context.Patients.AnyAsync(p => p.PatientId == patientId, cancellationToken);
        if (!exists)
            throw new KeyNotFoundException($"Patient {patientId} was not found.");

        var visit = new Visit
        {
            PatientId = patientId,
            RecordedAt = DateTime.UtcNow,
            VisitStart = DateTime.UtcNow,
            VisitStatus = request.VisitStatus,
            ReasonForVisit = request.ReasonForVisit,
            Notes = request.Notes
        };

        _context.Visits.Add(visit);
        await _context.SaveChangesAsync(cancellationToken);
        return visit;
    }

    public async Task<IReadOnlyList<Visit>> GetByPatientAsync(int patientId, CancellationToken cancellationToken = default) =>
        await _context.Visits
            .AsNoTracking()
            .Where(v => v.PatientId == patientId)
            .OrderByDescending(v => v.RecordedAt)
            .ToListAsync(cancellationToken);
}
