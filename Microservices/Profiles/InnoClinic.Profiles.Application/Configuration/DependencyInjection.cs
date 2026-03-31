using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InnoClinic.Profiles.Application.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
