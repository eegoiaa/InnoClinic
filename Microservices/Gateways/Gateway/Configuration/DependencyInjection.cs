namespace Gateway.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGatewayInfrastructure(this IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:3000", "https://localhost:8000")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
                });
            });

            return services;
        }
    }
}
