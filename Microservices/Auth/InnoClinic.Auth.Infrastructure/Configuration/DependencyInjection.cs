using InnoClinic.Auth.Application.Interfaces;
using InnoClinic.Auth.Domain.Entities;
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
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AuthDbContext>(options =>
            options.UseNpgsql(connectionString,
                x => x.MigrationsAssembly(typeof(AuthDbContext).Assembly.GetName().Name)
            )
        );

        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
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
