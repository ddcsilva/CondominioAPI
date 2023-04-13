using CondominioAPI.Application.Services;
using CondominioAPI.Infrastructure.Repositories;

namespace CondominioAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterRepositoriesAndServices(this IServiceCollection services)
        {
            services.AddScoped<ICondominioRepository, CondominioRepository>();
            services.AddScoped<ICondominioService, CondominioService>();

            return services;
        }
    }
}
