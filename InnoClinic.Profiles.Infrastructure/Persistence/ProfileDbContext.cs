using InnoClinic.Profiles.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.Infrastructure.Persistence;

public class ProfileDbContext : DbContext
{
    public ProfileDbContext(DbContextOptions<ProfileDbContext> options) : base(options) { }

    public DbSet<Doctor> Doctors { get; set; }  
    public DbSet<Office> Offices { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<ServiceCategory> ServiceCategories { get; set; }
    public DbSet<Service> Services { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>()
            .Property(d => d.FullName)
            .HasComputedColumnSql(@$"""{nameof(Doctor.LastName)}"" || ' ' || ""{nameof(Doctor.FirstName)}"" || ' ' || COALESCE(""{nameof(Doctor.MiddleName)}"", '')", stored: true);

        modelBuilder.Entity<ServiceCategory>(entity =>
        {
            entity.ToTable("ServiceCategories");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CategoryName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.TimeSlotSize).IsRequired();

            entity.HasMany(c => c.Services)
                .WithOne(s => s.Category)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("Services");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ServiceName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Price).IsRequired().HasPrecision(18, 2);
            entity.Property(e => e.IsActive).IsRequired().HasDefaultValue(true);

            entity.HasOne(s => s.Specialization)
                  .WithMany()
                  .HasForeignKey(s => s.SpecializationId)
                  .OnDelete(DeleteBehavior.SetNull);
        });
    }
}