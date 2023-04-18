using Gadz.Crawler.Imobiliario.Domain.Ofertas;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Gadz.Crawler.Imobiliario.Sqlite;

[ExcludeFromCodeCoverage]
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddSqlite(this IServiceCollection services)
    {
        return services
            .AddScoped<IOfertaRepository>((a) =>
            {
                string connectionString = @$"{AppDomain.CurrentDomain.BaseDirectory}\MonitorZap.db";
                return new OfertaDatabaseRepository(connectionString);
            });
    }
}