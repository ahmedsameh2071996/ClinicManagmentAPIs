using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagmentAPIs.Model;

[Table("PatientRegistration")]
public class PatientRegistration
{
    [Key]
    [Column("registration_id")]
    public int RegistrationId { get; set; }

    [Column("patient_id_FK")]
    public int PatientId { get; set; }

    [Column("registration_date")]
    public DateOnly RegistrationDate { get; set; }

    [Column("registration_status")]
    public string RegistrationStatus { get; set; } = "Active";

    [Column("verified_by_staff_user")]
    public string? VerifiedByStaffUser { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    public Patient? Patient { get; set; }
}
