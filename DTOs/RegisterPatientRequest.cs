namespace ClinicManagmentAPIs.DTOs;

public class RegisterPatientRequest
{
    public string RegistrationStatus { get; set; } = "Active";
    public string? VerifiedByStaffUser { get; set; }
    public string? Notes { get; set; }
}
