using InnoClinic.Services.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Wolverine;
using Wolverine.RabbitMQ;

namespace InnoClinic.Services.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ServicesDbContext>(options =>
            options.UseNpgsql(connectionString, 
            x => x.MigrationsAssembly(typeof(ServicesDbContext).Assembly.GetName().Name)));
        return services;
    }

    public static IHostBuilder AddMessaging(this IHostBuilder host, Assembly applicationAssembly, IConfiguration configuration)
    {
        return host.UseWolverine(options =>
        {
            options.Discovery.IncludeAssembly(applicationAssembly);
            var rabbitUri = configuration.GetConnectionString("RabbitMq")
                            ?? throw new InvalidOperationException("RabbitMq connection string is missing!");
            options.UseRabbitMq(new Uri(rabbitUri))
                   .AutoProvision()
                   .DeclareExchange("specialization-updates", exchange =>
                   {
                       exchange.ExchangeType = ExchangeType.Fanout;
                   })
                   .BindExchange("specialization-updates")
                   .ToQueue("services-specializations");
            options.ListenToRabbitQueue("services-specializations");
        });
    }
}
