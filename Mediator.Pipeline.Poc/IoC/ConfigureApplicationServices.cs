using Mediator.Pipeline.Poc.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mediator.Pipeline.Poc.IoC
{
    public static class ConfigureApplicationServices
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ConfigureApplicationServices).Assembly));

            services.AddScoped<IPipelineService, PipelineService>();
            
            return services;
        }
    }
}