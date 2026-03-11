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
        services.AddOptions<AuthOptions>()
            .Bind(configuration.GetSection(AuthOptions.SectionName))
            .Validate(a => !string.IsNullOrWhiteSpace(a.FrontendConfirmationUrl), "Frontend confirmation URL is missing or empty in configuration.")
            .ValidateOnStart();

        services.Configure<SmtpOptions>(configuration.GetSection(SmtpOptions.SectionName));

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
