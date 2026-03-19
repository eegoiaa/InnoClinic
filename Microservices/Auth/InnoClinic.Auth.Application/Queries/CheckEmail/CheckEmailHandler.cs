using InnoClinic.Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace InnoClinic.Auth.Application.Queries.CheckEmail;

public static class CheckEmailHandler
{
    public static async Task<bool> Handle(
    CheckEmailQuery query,
    UserManager<ApplicationUser> userManager)
    {
        var user = await userManager.FindByEmailAsync(query.Email);
        return user != null;
    }
}
