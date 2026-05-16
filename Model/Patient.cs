using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagmentAPIs.Model;

[Table("Patient")]
public class Patient
{
    [Key]
    [Column("patient_id")]
    public int PatientId { get; set; }

    [Column("mrn")]
    public string Mrn { get; set; } = string.Empty;

    [Column("first_name")]
    public string FirstName { get; set; } = string.Empty;

    [Column("last_name")]
    public string LastName { get; set; } = string.Empty;

    [Column("date_of_birth")]
    public DateOnly DateOfBirth { get; set; }

    [Column("sex")]
    public string Sex { get; set; } = string.Empty;

    [Column("phone")]
    public string? Phone { get; set; }

    [Column("email")]
    public string? Email { get; set; }

    [Column("address")]
    public string? Address { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    public ICollection<PatientRegistration> Registrations { get; set; } = [];
    public ICollection<Visit> Visits { get; set; } = [];
    public ICollection<Vitals> Vitals { get; set; } = [];
}
