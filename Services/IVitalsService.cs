using ClinicManagmentAPIs.DTOs;
using ClinicManagmentAPIs.Model;

namespace ClinicManagmentAPIs.Services;

public interface IVitalsService
{
    Task<Vitals> CreateAsync(CreateVitalsRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Vitals>> GetByPatientAsync(int patientId, CancellationToken cancellationToken = default);
}
