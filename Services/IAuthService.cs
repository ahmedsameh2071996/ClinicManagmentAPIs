using ClinicManagmentAPIs.DTOs;

namespace ClinicManagmentAPIs.Services;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
}
