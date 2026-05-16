namespace ClinicManagmentAPIs.Auth;

public interface ICurrentUser
{
    int? UserId { get; }
    string? Username { get; }
    string? Role { get; }
    bool IsAuthenticated { get; }
}
