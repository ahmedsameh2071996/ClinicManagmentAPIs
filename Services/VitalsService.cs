using ClinicManagmentAPIs.Data;
using ClinicManagmentAPIs.DTOs;
using ClinicManagmentAPIs.Model;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagmentAPIs.Services;

public class VitalsService : IVitalsService
{
    private readonly DBContext _context;

    public VitalsService(DBContext context)
    {
        _context = context;
    }

    public async Task<Vitals> CreateAsync(CreateVitalsRequest request, CancellationToken cancellationToken = default)
    {
        var visit = await _context.Visits
            .FirstOrDefaultAsync(v => v.VisitId == request.VisitId, cancellationToken);

        if (visit is null)
            throw new KeyNotFoundException($"Visit {request.VisitId} was not found.");

        if (visit.PatientId != request.PatientId)
            throw new InvalidOperationException("PatientId does not match the visit's patient.");

        if (request.RecordedByDoctorId is int doctorId &&
            !await _context.Doctors.AnyAsync(d => d.DoctorId == doctorId && d.ActiveFlag, cancellationToken))
        {
            throw new KeyNotFoundException($"Doctor {doctorId} was not found or is inactive.");
        }

        var vitals = new Vitals
        {
            VisitId = request.VisitId,
            PatientId = request.PatientId,
            RecordedByDoctorId = request.RecordedByDoctorId,
            HeightCm = request.HeightCm,
            WeightKg = request.WeightKg,
            TemperatureC = request.TemperatureC,
            PulseBpm = request.PulseBpm,
            RespiratoryRate = request.RespiratoryRate,
            SystolicBp = request.SystolicBp,
            DiastolicBp = request.DiastolicBp,
            Spo2Percent = request.Spo2Percent,
            PainScore = request.PainScore,
            Notes = request.Notes,
            RecordedAt = DateTime.UtcNow
        };

        _context.Vitals.Add(vitals);

        if (request.WeightKg.HasValue)
            visit.WeightKg = request.WeightKg;
        if (request.TemperatureC.HasValue)
            visit.TemperatureC = request.TemperatureC;
        if (request.PulseBpm.HasValue)
            visit.PulseBpm = request.PulseBpm;
        if (request.RespiratoryRate.HasValue)
            visit.RespiratoryRate = request.RespiratoryRate;
        if (request.SystolicBp.HasValue)
            visit.SystolicBp = request.SystolicBp;
        if (request.DiastolicBp.HasValue)
            visit.DiastolicBp = request.DiastolicBp;
        if (request.Spo2Percent.HasValue)
            visit.Spo2Percent = request.Spo2Percent;
        if (request.PainScore.HasValue)
            visit.PainScore = request.PainScore;

        await _context.SaveChangesAsync(cancellationToken);
        return vitals;
    }

    public async Task<IReadOnlyList<Vitals>> GetByPatientAsync(int patientId, CancellationToken cancellationToken = default) =>
        await _context.Vitals
            .AsNoTracking()
            .Where(v => v.PatientId == patientId)
            .OrderByDescending(v => v.RecordedAt)
            .ToListAsync(cancellationToken);
}
