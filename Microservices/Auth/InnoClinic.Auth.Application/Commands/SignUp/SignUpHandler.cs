using InnoClinic.Auth.Application.Interfaces;
using InnoClinic.Auth.Domain.Constants;
using InnoClinic.Auth.Domain.Entities;
using InnoClinic.Auth.Domain.Exceptions;
using InnoClinic.Auth.Domain.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace InnoClinic.Auth.Application.Commands.SignUp;

public static class SignUpHandler
{
    public static async Task Handle(
        SignUpCommand command,
        UserManager<ApplicationUser> userManager,
        IEmailService emailService,
        IOptions<AuthOptions> options,
        CancellationToken cancellationToken
        )
    {
        var user = new ApplicationUser
        {
            UserName = command.Email,
            Email = command.Email
        };

        var result = await userManager.CreateAsync(user, command.Password);

        if (!result.Succeeded)
            throw new IdentityException(result.Errors);

        var roleResult = await userManager.AddToRoleAsync(user, RolesConstants.Patient);

        if (!roleResult.Succeeded)
            throw new IdentityException(roleResult.Errors);

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

        var settings = options.Value;
        var confirmationLink = $"{settings.FrontendConfirmationUrl}?userId={user.Id}&token={Uri.EscapeDataString(token)}";

        await emailService.SendConfirmationLinkAsync(user.Email, confirmationLink, cancellationToken);
    }
}
