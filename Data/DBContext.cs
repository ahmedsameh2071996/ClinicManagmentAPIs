using ClinicManagmentAPIs.Model;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagmentAPIs.Data;

public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options) : base(options) { }

    public DbSet<UserAccount> User => Set<UserAccount>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<PatientRegistration> PatientRegistrations => Set<PatientRegistration>();
    public DbSet<Visit> Visits => Set<Visit>();
    public DbSet<Vitals> Vitals => Set<Vitals>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.ToTable("UserAccount");
            entity.HasKey(e => e.user_id);
        });

        modelBuilder.Entity<PatientRegistration>(entity =>
        {
            entity.HasOne(r => r.Patient)
                .WithMany(p => p.Registrations)
                .HasForeignKey(r => r.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.HasOne(v => v.Patient)
                .WithMany(p => p.Visits)
                .HasForeignKey(v => v.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Vitals>(entity =>
        {
            entity.HasOne(v => v.Visit)
                .WithMany(vt => vt.Vitals)
                .HasForeignKey(v => v.VisitId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(v => v.Patient)
                .WithMany(p => p.Vitals)
                .HasForeignKey(v => v.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(v => v.RecordedByDoctor)
                .WithMany()
                .HasForeignKey(v => v.RecordedByDoctorId)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }
}
