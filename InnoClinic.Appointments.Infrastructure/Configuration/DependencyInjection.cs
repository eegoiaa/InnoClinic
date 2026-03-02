using InnoClinic.Appointments.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Reflection;
using Wolverine;
using Wolverine.FluentValidation;
using Wolverine.RabbitMQ;

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

    public static IHostBuilder AddMesseging(this IHostBuilder host, Assembly applicationAssembly, IConfiguration configuration)
    {
        return host.UseWolverine(options =>
        {
            options.Discovery.IncludeAssembly(applicationAssembly);

            var rabbitUri = configuration.GetConnectionString("RabbitMq")
                            ?? throw new InvalidOperationException("RabbitMq connection string is missing!");

            options.UseRabbitMq(new Uri(rabbitUri))
                   .AutoProvision();

            options.UseRabbitMq()
                   .BindExchange("doctor-created-exchange", exchange => exchange.ExchangeType = ExchangeType.Fanout)
                   .ToQueue("appointments-doctor-sync");

            options.UseRabbitMq()
                   .BindExchange("service-created-exchange", exchange => exchange.ExchangeType = ExchangeType.Fanout)
                   .ToQueue("appointments-service-sync");

            options.ListenToRabbitQueue("appointments-doctor-sync");
            options.ListenToRabbitQueue("appointments-service-sync");

            options.UseFluentValidation();
            options.Policies.AutoApplyTransactions();
        });
    }
}
