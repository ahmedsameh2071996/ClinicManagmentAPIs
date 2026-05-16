namespace ClinicManagmentAPIs.DTOs;

public class CreateVitalsRequest
{
    public int VisitId { get; set; }
    public int PatientId { get; set; }
    public int? RecordedByDoctorId { get; set; }
    public decimal? HeightCm { get; set; }
    public decimal? WeightKg { get; set; }
    public decimal? TemperatureC { get; set; }
    public int? PulseBpm { get; set; }
    public int? RespiratoryRate { get; set; }
    public int? SystolicBp { get; set; }
    public int? DiastolicBp { get; set; }
    public int? Spo2Percent { get; set; }
    public int? PainScore { get; set; }
    public string? Notes { get; set; }
}
