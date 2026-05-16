using ClinicManagmentAPIs.Auth;
using ClinicManagmentAPIs.Data;
using ClinicManagmentAPIs.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagmentAPIs.Services;

public class AuthService : IAuthService
{
    private readonly DBContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(
        DBContext context,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _context.User
            .AsNoTracking()
            .FirstOrDefaultAsync(
                u => u.username == request.Username && u.active_flag,
                cancellationToken);

        if (user is null || !_passwordHasher.Verify(request.Password, user.password_hash ?? string.Empty))
            return null;

        return new LoginResponse
        {
            Token = _jwtTokenService.CreateToken(user),
            UserId = user.user_id,
            Username = user.username ?? string.Empty,
            Role = user.role ?? string.Empty
        };
    }
}
