namespace InnoClinic.Auth.Application.Commands.ConfirmEmail;

public record ConfirmEmailCommand(
    Guid UserId,
    string Token
    );

