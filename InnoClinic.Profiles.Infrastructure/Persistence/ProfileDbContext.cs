using InnoClinic.Profiles.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.Infrastructure.Persistence;

public class ProfileDbContext : DbContext
{
    public ProfileDbContext(DbContextOptions<ProfileDbContext> options) : base(options) { }

    public DbSet<Doctor> Doctors { get; set; }  
    public DbSet<Office> Offices { get; set; }
    public DbSet<Specialization> Specializations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>()
            .Property(d => d.FullName)
            .HasComputedColumnSql(@$"""{nameof(Doctor.LastName)}"" || ' ' || ""{nameof(Doctor.FirstName)}"" || ' ' || COALESCE(""{nameof(Doctor.MiddleName)}"", '')", stored: true);
    }
}