using InnoClinic.Auth.Domain.Entities;
using InnoClinic.Auth.Domain.Models;

namespace InnoClinic.Auth.Application.Interfaces;

public interface IJwtProvider
{
    string GenerateAccessToken(ApplicationUser appUser, IEnumerable<string> roles);
    RefreshToken GenerateRefreshToken();
}
