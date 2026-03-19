using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace InnoClinic.Common.Extensions;

public static class JwtAuthenticationExtensions
{
    public static IServiceCollection AddRsaJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var publicKeyPem = jwtSettings["PublicKey"];

        if(string.IsNullOrWhiteSpace(publicKeyPem))
            throw new InvalidOperationException("Public key for JWT is missing in configuration.");

        var rsa = RSA.Create();
        rsa.ImportFromPem(publicKeyPem);
        var rsaKey = new RsaSecurityKey(rsa);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],

                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],

                    ValidateLifetime = true,
                    IssuerSigningKey = rsaKey,
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
}
