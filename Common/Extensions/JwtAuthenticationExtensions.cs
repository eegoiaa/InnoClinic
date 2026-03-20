using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using InnoClinic.Common.Options;

namespace InnoClinic.Common.Extensions;

public static class JwtAuthenticationExtensions
{
    public static IServiceCollection AddRsaJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection("JwtSettings"));
        var jwtOptions = configuration.GetSection("JwtSettings").Get<JwtOptions>();

        if (jwtOptions == null || string.IsNullOrWhiteSpace(jwtOptions.PublicKey))
            throw new InvalidOperationException("JWT Public Key is missing in configuration.");

        var rsa = RSA.Create();
        rsa.ImportFromPem(jwtOptions.PublicKey);
        var rsaKey = new RsaSecurityKey(rsa);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,

                    ValidateLifetime = true,
                    IssuerSigningKey = rsaKey,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["access_token"];
                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }
}
