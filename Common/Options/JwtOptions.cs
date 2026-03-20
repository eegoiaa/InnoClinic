namespace InnoClinic.Common.Options;

public class JwtOptions
{
    public required string PublicKey { get; init; }
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
}
