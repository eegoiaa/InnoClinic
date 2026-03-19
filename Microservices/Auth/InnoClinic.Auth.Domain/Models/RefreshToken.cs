namespace InnoClinic.Auth.Domain.Models;

public record RefreshToken(
    string Token,
    DateTime ExpiryTime
    );

