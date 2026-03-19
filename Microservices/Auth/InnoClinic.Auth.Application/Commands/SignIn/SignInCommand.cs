namespace InnoClinic.Auth.Application.Commands.SignIn;

public record SignInCommand(
    string Email,
    string Password
    );

