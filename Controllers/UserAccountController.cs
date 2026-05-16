using ClinicManagmentAPIs.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagmentAPIs.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserAccountController : ControllerBase
{
    private readonly DBContext _context;

    public UserAccountController(DBContext context)
    {
        _context = context;
    }

    [HttpGet("{user_id}")]
    public IActionResult GetUser(int user_id)
    {
        var user = _context.User.FirstOrDefault(u => u.user_id == user_id);
        if (user == null)
            return NotFound(new { message = "User not found" });

        return Ok(user);
    }
}
