using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;

namespace InnoClinic.Appointments.Application.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddAplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
