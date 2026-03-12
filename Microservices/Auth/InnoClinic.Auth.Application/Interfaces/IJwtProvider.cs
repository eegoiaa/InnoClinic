using InnoClinic.Auth.Domain.Entities;

namespace InnoClinic.Auth.Application.Interfaces;

public interface IJwtProvider
{
    string GenerateAccessToken(ApplicationUser appUser, IEnumerable<string> roles);
    string GenerateRefreshToken();
}
