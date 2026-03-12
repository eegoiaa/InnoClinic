using InnoClinic.Auth.Domain.Entities;
using InnoClinic.Auth.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace InnoClinic.Auth.Application.Commands.ConfirmEmail;

public static class ConfirmEmailHandler
{
    public static async Task Handle(
        ConfirmEmailCommand command,
        UserManager<ApplicationUser> userManager
        )
    {
        var user = await userManager.FindByIdAsync(command.UserId.ToString()) ?? throw new IdentityException("User not found");

        var result = await userManager.ConfirmEmailAsync(user, command.Token);

        if (!result.Succeeded)
            throw new IdentityException(result.Errors);
    }
}
