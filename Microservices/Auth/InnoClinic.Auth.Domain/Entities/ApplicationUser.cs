using Microsoft.AspNetCore.Identity;

namespace InnoClinic.Auth.Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}
