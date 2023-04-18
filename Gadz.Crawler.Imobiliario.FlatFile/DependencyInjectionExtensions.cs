using Gadz.Crawler.Imobiliario.Domain.Ofertas;
using Gadz.Crawler.Imobiliario.Domain.Pesquisas;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Gadz.Crawler.Imobiliario.FlatFile;

[ExcludeFromCodeCoverage]
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddFlatFileRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IOfertaRepository, OfertaFileRepository>()
            .AddScoped<IPesquisasRepository, OfertaFileRepository>();
    }
}
