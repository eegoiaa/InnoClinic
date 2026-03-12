namespace InnoClinic.Auth.Domain.Settings;

public class JwtOptions
{
    public const string SectionName = "JwtSettings";
    public required string SecretKey { get; init; }
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required int AccessTokenExpirationMinutes { get; init; }
    public required int RefreshTokenExpirationDays { get; init; }
}
