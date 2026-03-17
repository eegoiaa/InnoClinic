using InnoClinic.Auth.Domain.Models;

namespace InnoClinic.Auth.Application.Commands.SignIn;

public record SignInResult(
    string AccessToken,
    RefreshToken RefreshToken
    );

