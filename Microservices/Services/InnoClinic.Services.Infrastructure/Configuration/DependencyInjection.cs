using InnoClinic.Services.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InnoClinic.Services.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ServicesDbContext>(options => options.UseNpgsql(connectionString, x =>
            x.MigrationsAssembly(typeof(ServicesDbContext).Assembly.FullName)));
        return services;
    }
}
