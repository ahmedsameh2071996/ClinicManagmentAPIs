using ClinicManagmentAPIs.DTOs;
using ClinicManagmentAPIs.Model;

namespace ClinicManagmentAPIs.Services;

public interface IVisitService
{
    Task<Visit> CreateForPatientAsync(int patientId, CreateVisitRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Visit>> GetByPatientAsync(int patientId, CancellationToken cancellationToken = default);
}
