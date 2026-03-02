using InnoClinic.Appointments.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Appointments.Infrastructure.Persistence;

public class AppointmentDbContext : DbContext
{
    public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options) : base(options) { }
    
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<DoctorReference> DoctorReferences { get; set; }
    public DbSet<ServiceReference> ServiceReferences { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne<DoctorReference>()
                .WithMany()
                .HasForeignKey(e => e.DoctorId);

            entity.HasOne<ServiceReference>()
                .WithMany()
                .HasForeignKey(e => e.ServiceId);
        });
    }
}
