using InnoClinic.Profiles.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.Infrastructure.Persistence;

public class ProfileDbContext : DbContext
{
    public ProfileDbContext(DbContextOptions<ProfileDbContext> options) : base(options) { }

    public DbSet<Doctor> Doctors { get; set; }  
    public DbSet<Office> Offices { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<Patient> Patients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>()
            .Property(d => d.FullName)
            .HasComputedColumnSql(@$"""{nameof(Doctor.LastName)}"" || ' ' || ""{nameof(Doctor.FirstName)}"" || ' ' || COALESCE(""{nameof(Doctor.MiddleName)}"", '')", stored: true);

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(p => p.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(p => p.DateOfBirth)
                .IsRequired();

            entity.Property(p => p.IsLinkedToAccount)
               .HasDefaultValue(false);

            entity.HasIndex(p => new { p.FirstName, p.LastName, p.DateOfBirth });

        });
            
    }
}