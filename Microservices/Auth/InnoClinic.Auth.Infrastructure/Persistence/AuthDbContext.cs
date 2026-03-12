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

        var adminRoleId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var doctorRoleId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var patientRoleId = Guid.Parse("33333333-3333-3333-3333-333333333333");

        builder.Entity<IdentityRole<Guid>>().HasData(
            new IdentityRole<Guid> { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole<Guid> { Id = doctorRoleId, Name = "Doctor", NormalizedName = "DOCTOR" },
            new IdentityRole<Guid> { Id = patientRoleId, Name = "Patient", NormalizedName = "PATIENT" }
        );
    }
}
