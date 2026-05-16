using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagmentAPIs.Model;

[Table("Vitals")]
public class Vitals
{
    [Key]
    [Column("vitals_id")]
    public int VitalsId { get; set; }

    [Column("visit_id_FK")]
    public int VisitId { get; set; }

    [Column("patient_id_FK")]
    public int PatientId { get; set; }

    [Column("recorded_by_doctor_id_FK")]
    public int? RecordedByDoctorId { get; set; }

    [Column("height_cm")]
    public decimal? HeightCm { get; set; }

    [Column("weight_kg")]
    public decimal? WeightKg { get; set; }

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

    [Column("recorded_at")]
    public DateTime RecordedAt { get; set; }

    public Visit? Visit { get; set; }
    public Patient? Patient { get; set; }
    public Doctor? RecordedByDoctor { get; set; }
}
