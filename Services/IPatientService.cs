using ClinicManagmentAPIs.DTOs;
using ClinicManagmentAPIs.Model;

namespace ClinicManagmentAPIs.Services;

public interface IPatientService
{
    Task<Patient> CreateAsync(CreatePatientRequest request, CancellationToken cancellationToken = default);
    Task<Patient?> GetByIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Patient>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PatientRegistration> RegisterAsync(int patientId, RegisterPatientRequest request, CancellationToken cancellationToken = default);
}
