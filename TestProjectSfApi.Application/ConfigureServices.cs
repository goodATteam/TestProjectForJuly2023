using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TestProjectSfApi.Application.Common.Factories;

namespace TestProjectSfApi.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
            services.AddTransient<IClientFactory, ClientFactory>();

        return services;
    }
}