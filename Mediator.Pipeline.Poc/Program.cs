using Mediator.Pipeline.Poc.IoC;
using Mediator.Pipeline.Poc.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Mediator.Pipeline.Poc
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var service = host.Services.GetRequiredService<IPipelineService>();
            await service.Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((host, services) => { ConfigureServices(host, services); })
                .ConfigureLogging(logging => { logging.ClearProviders(); });
        }

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddApplicationServices(host.Configuration);
        }
    }
}