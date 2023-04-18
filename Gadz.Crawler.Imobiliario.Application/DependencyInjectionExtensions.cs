using Gadz.Crawler.Imobiliario.Application.UseCases;
using Gadz.Crawler.Imobiliario.FlatFile;
using Gadz.Crawler.Imobiliario.Zap;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Gadz.Crawler.Imobiliario.Application;

[ExcludeFromCodeCoverage]
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddCrawler(this IServiceCollection services)
    {
        return services
            .AddFlatFileRepositories()
            .AddZap()
            .AddScoped<BuscarOfertasUseCase>()
            ;
    }
}
