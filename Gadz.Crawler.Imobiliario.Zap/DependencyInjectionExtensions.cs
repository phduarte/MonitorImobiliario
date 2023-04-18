using Gadz.Crawler.Imobiliario.Domain.Pesquisas;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Gadz.Crawler.Imobiliario.Zap;

[ExcludeFromCodeCoverage]
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddZap(this IServiceCollection services)
    {
        return services.AddScoped<IPlataformaDeBusca, ZapAdapter>();
    }
}