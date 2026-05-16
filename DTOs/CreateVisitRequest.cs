namespace ClinicManagmentAPIs.DTOs;

public class CreateVisitRequest
{
    public string? ReasonForVisit { get; set; }
    public string VisitStatus { get; set; } = "Open";
    public string? Notes { get; set; }
}
