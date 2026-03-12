using InnoClinic.Auth.Application.Interfaces;
using InnoClinic.Auth.Domain.Constants;
using InnoClinic.Auth.Domain.Entities;
using InnoClinic.Auth.Domain.Settings;
using InnoClinic.Auth.Infrastructure.Persistence;
using InnoClinic.Auth.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Wolverine;

namespace InnoClinic.Auth.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<DatabaseOptions>()
            .Bind(configuration.GetSection(DatabaseOptions.SectionName))
            .Validate(d => !string.IsNullOrWhiteSpace(d.DefaultConnection), "Database: Connection string 'DefaultConnection' is missing or empty.")
            .ValidateOnStart();

        services.AddOptions<AuthOptions>()
            .Bind(configuration.GetSection(AuthOptions.SectionName))
            .Validate(a => !string.IsNullOrWhiteSpace(a.FrontendConfirmationUrl), "Frontend confirmation URL is missing or empty in configuration.")
            .ValidateOnStart();

        services.AddOptions<SmtpOptions>()
            .Bind(configuration.GetSection(SmtpOptions.SectionName))
            .Validate(s => !string.IsNullOrWhiteSpace(s.Host), "SMTP: Host is required.")
            .Validate(s => s.Port > 0, "SMTP: Port must be a positive number.")
            .Validate(s => !string.IsNullOrWhiteSpace(s.Username), "SMTP: Username is required.")
            .Validate(s => !string.IsNullOrWhiteSpace(s.Password), "SMTP: Password is required.")
            .Validate(s => !string.IsNullOrWhiteSpace(s.FromEmail), "SMTP: FromEmail is required.")
            .ValidateOnStart();

        services.AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(JwtOptions.SectionName))
            .Validate(j => !string.IsNullOrWhiteSpace(j.SecretKey) && j.SecretKey.Length >= 16, "JWT: SecretKey must be at least 16 characters long.")
            .Validate(o => !string.IsNullOrWhiteSpace(o.Issuer), "JWT: Issuer is required.")
            .Validate(o => !string.IsNullOrWhiteSpace(o.Audience), "JWT: Audience is required.")
            .Validate(o => o.AccessTokenExpirationMinutes > 0, "JWT: AccessTokenExpiration must be positive.")
            .ValidateOnStart();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AuthDbContext>(options =>
            options.UseNpgsql(connectionString,
                x => x.MigrationsAssembly(typeof(AuthDbContext).Assembly.GetName().Name)
            )
        );

        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
        {
            options.Password.RequiredLength = AuthConstants.RequiredLength;
            options.Password.RequireDigit = AuthConstants.RequireDigit;
            options.Password.RequireLowercase = AuthConstants.RequireLowercase;
            options.Password.RequireUppercase = AuthConstants.RequireUppercase;
            options.Password.RequireNonAlphanumeric = AuthConstants.RequireNonAlphanumeric;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<AuthDbContext>()
        .AddDefaultTokenProviders();

        services.AddTransient<IEmailService, EmailService>();
        services.AddScoped<IJwtProvider, JwtProvider>();

        return services;
    }

    public static IHostBuilder AddMessaging(this IHostBuilder host, Assembly assembly, IConfiguration configuration)
    {
        return host.UseWolverine(options =>
        {
            options.Discovery.IncludeAssembly(assembly);
        });
    }
}
