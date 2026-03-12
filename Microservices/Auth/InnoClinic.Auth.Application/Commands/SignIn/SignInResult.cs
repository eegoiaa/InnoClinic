namespace InnoClinic.Auth.Application.Commands.SignIn;

public record SignInResult(
    string AccessToken,
    string RefreshToken
    );

