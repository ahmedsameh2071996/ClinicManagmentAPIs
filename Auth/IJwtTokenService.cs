using ClinicManagmentAPIs.Model;

namespace ClinicManagmentAPIs.Auth;

public interface IJwtTokenService
{
    string CreateToken(UserAccount user);
}
