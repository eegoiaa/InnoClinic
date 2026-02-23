using InnoClinic.Appointments.Infrastructure.Persistence;
using JasperFx.Core.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InnoClinic.Appointments.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppointmentDbContext>(options =>
        {
            options.UseNpgsql(connectionString,
                x => x.MigrationsAssembly(typeof(AppointmentDbContext).Assembly.GetName().Name));
        });
        return services;
    }
}
