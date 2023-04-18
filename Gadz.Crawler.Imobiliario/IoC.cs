using Gadz.Crawler.Imobiliario.Application;
using Gadz.Crawler.Imobiliario.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Gadz.Crawler.Imobiliario.Worker
{
    public static class IoC
    {
        public static ServiceProvider ConfigureDependencyInjection()
        {
            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddConsole();
            });
            services.AddScoped<BuscarOfertasUseCase>();
            services.AddCrawler();

            return services.BuildServiceProvider();
        }
    }
}
