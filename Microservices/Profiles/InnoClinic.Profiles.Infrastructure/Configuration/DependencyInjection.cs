using Common.Events;
using InnoClinic.Profiles.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Wolverine;
using Wolverine.RabbitMQ;

namespace InnoClinic.Profiles.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ProfileDbContext>(options =>
            options.UseNpgsql(connectionString, 
            x => x.MigrationsAssembly(typeof(ProfileDbContext).Assembly.GetName().Name)));

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
                   .AutoProvision();
            options.PublishMessage<SpecializationCreatedEvent>()
                   .ToRabbitExchange("specialization-updates", exchange =>
                   {
                       exchange.ExchangeType = ExchangeType.Fanout;
                   });
        });
    }
}
