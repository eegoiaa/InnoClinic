using InnoClinic.Auth.Application.Interfaces;
using InnoClinic.Auth.Domain.Entities;
using InnoClinic.Auth.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace InnoClinic.Auth.Application.Commands.SignUp;

public static class SignUpHandler
{
    public static async Task Handle(
        SignUpCommand command,
        UserManager<ApplicationUser> userManager,
        IEmailService emailService,
        IConfiguration configuration,
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
        {
            throw new IdentityException(result.Errors);
        }

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

        var frontendUrl = configuration["AuthSettings:FrontendConfirmationUrl"]
            ?? throw new IdentityException("Frontend confirmation URL is not configured");

        var confirmationLink = $"{frontendUrl}?userId={user.Id}&token={Uri.EscapeDataString(token)}";

        await emailService.SendConfirmationLinkAsync(user.Email, confirmationLink);
    }
}
