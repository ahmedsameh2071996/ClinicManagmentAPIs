using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagmentAPIs.Model;

[Table("Visit")]
public class Visit
{
    [Key]
    [Column("visit_id")]
    public int VisitId { get; set; }

    [Column("patient_id_FK")]
    public int PatientId { get; set; }

    [Column("appointment_id_FK")]
    public int? AppointmentId { get; set; }

    [Column("recorded_at")]
    public DateTime RecordedAt { get; set; }

    [Column("weight_kg")]
    public decimal? WeightKg { get; set; }

    [Column("bmi")]
    public decimal? Bmi { get; set; }

    [Column("temperature_c")]
    public decimal? TemperatureC { get; set; }

    [Column("pulse_bpm")]
    public int? PulseBpm { get; set; }

    [Column("respiratory_rate")]
    public int? RespiratoryRate { get; set; }

    [Column("systolic_bp")]
    public int? SystolicBp { get; set; }

    [Column("diastolic_bp")]
    public int? DiastolicBp { get; set; }

    [Column("spo2_percent")]
    public int? Spo2Percent { get; set; }

    [Column("pain_score")]
    public int? PainScore { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [Column("visit_start")]
    public DateTime? VisitStart { get; set; }

    [Column("visit_end")]
    public DateTime? VisitEnd { get; set; }

    [Column("visit_status")]
    public string VisitStatus { get; set; } = "Open";

    [Column("reason_for_visit")]
    public string? ReasonForVisit { get; set; }

    public Patient? Patient { get; set; }
    public ICollection<Vitals> Vitals { get; set; } = [];
}
