namespace InnoClinic.Auth.Application.Commands.SignUp;

public record SignUpCommand(
    string Email,
    string Password,
    string ConfirmPassword
    );

