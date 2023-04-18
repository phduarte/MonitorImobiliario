using Gadz.Crawler.Imobiliario.Application.UseCases;
using Gadz.Crawler.Imobiliario.Domain.Pesquisas;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Gadz.Crawler.Imobiliario.Worker;

class Program
{
    const string APP_TITLE = "Crawler de Imóveis";

    static async Task Main(string[] args)
    {
        var serviceProvider = IoC.ConfigureDependencyInjection();
        var logger = serviceProvider.GetService<ILogger<Program>>() ?? throw new NotImplementedException("ILogger<Program>");
        var useCase = serviceProvider.GetService<BuscarOfertasUseCase>() ?? throw new NotImplementedException("BuscarOfertasUseCase");

        if (useCase == null)
        {
            return;
        }

        useCase.OnStatusChanged += (message) => logger.LogInformation(message);
        useCase.OnErrorOccurred += (ex) => logger.LogError(ex, "");

        do
        {
            ResetScreen();

            try
            {
                string estado = GetParameter("Em qual estado você deseja buscar?");
                string cidade = GetParameter("Em qual cidade de " + estado + " você deseja buscar?");

                Console.Clear();

                BuscarOfertasInput input = new()
                {
                    Dormitorios = 2,
                    Vagas = 1,
                    Cidade = cidade,
                    Estado = estado,
                    Ordenacao = CriterioDeOrdenacao.Valor
                };

                await useCase.Executar(input);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
            }

        } while (true);
    }

    private static void ResetScreen()
    {
        Console.Title = APP_TITLE;
        Console.Clear();
    }

    private static string GetParameter(string message)
    {
        Console.Write("{0} ", message);

        return Console.ReadLine() ?? throw new ArgumentNullException(message);
    }
}
