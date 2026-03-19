using InnoClinic.Auth.Domain.Constants;
using InnoClinic.Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Auth.Infrastructure.Persistence;

public class AuthDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
	public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(entity => entity.ToTable("Users"));
        builder.Entity<IdentityRole<Guid>>(entity => entity.ToTable("Roles"));
        builder.Entity<IdentityUserRole<Guid>>(entity => entity.ToTable("UserRoles"));
        builder.Entity<IdentityUserClaim<Guid>>(entity => entity.ToTable("UserClaims"));
        builder.Entity<IdentityUserLogin<Guid>>(entity => entity.ToTable("UserLogins"));
        builder.Entity<IdentityRoleClaim<Guid>>(entity => entity.ToTable("RoleClaims"));
        builder.Entity<IdentityUserToken<Guid>>(entity => entity.ToTable("UserTokens"));

        builder.Entity<IdentityRole<Guid>>().HasData(
            new IdentityRole<Guid> { Id = RolesConstants.AdminId, Name = RolesConstants.Admin, NormalizedName = RolesConstants.Admin.ToUpper() },
            new IdentityRole<Guid> { Id = RolesConstants.DoctorId, Name = RolesConstants.Doctor, NormalizedName = RolesConstants.Doctor.ToUpper() },
            new IdentityRole<Guid> { Id = RolesConstants.PatientId, Name = RolesConstants.Patient, NormalizedName = RolesConstants.Patient.ToUpper() },
            new IdentityRole<Guid> { Id = RolesConstants.ReceptionistId, Name = RolesConstants.Receptionist, NormalizedName = RolesConstants.Receptionist.ToUpper() }
        );
    }
}
