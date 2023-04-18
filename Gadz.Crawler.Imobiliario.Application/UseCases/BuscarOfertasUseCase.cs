using Gadz.Crawler.Imobiliario.Domain.Ofertas;
using Gadz.Crawler.Imobiliario.Domain.Pesquisas;

namespace Gadz.Crawler.Imobiliario.Application.UseCases
{
    public delegate void StatusChanged(string message);
    public delegate void ErrorOcurred(Exception ex);

    public class BuscarOfertasUseCase : UseCase<BuscarOfertasInput, DefaultOutput>
    {
        readonly IOfertaRepository _ofertaRepository;
        readonly IPesquisasRepository _pesquisasRepository;
        readonly IPlataformaDeBusca _plataformaDeBusca;

        public event StatusChanged OnStatusChanged;
        public event ErrorOcurred OnErrorOccurred;

        public BuscarOfertasUseCase(
            IOfertaRepository ofertaRepository,
            IPesquisasRepository pesquisasRepository,
            IPlataformaDeBusca plataformaDeBusca)
        {
            _ofertaRepository = ofertaRepository;
            _pesquisasRepository = pesquisasRepository;
            _plataformaDeBusca = plataformaDeBusca;
        }

        public override async Task<DefaultOutput> Executar(BuscarOfertasInput input)
        {
            //OnStatusChanged?.Invoke($"Busca de ofertas iniciada");

            try
            {
                await _ofertaRepository.Clear();

                var criterios = input.AsCriteriosDeBusca();

                var pesquisa = await _plataformaDeBusca.BuscarOfertas(criterios);

                OnStatusChanged?.Invoke($"{pesquisa.Ofertas.Count} resultados encontrados");

                await _pesquisasRepository.Add(pesquisa);

                OnStatusChanged?.Invoke("Pesquisa salva no banco de dados");

                await _ofertaRepository.AddRange(pesquisa.Ofertas);

                OnStatusChanged?.Invoke($"Ofertas salvas no banco de dados");
            }
            catch (Exception ex)
            {
                OnErrorOccurred?.Invoke(ex);
            }

            //OnStatusChanged?.Invoke($"Busca de ofertas concluída");
            return DefaultOutput.Success();
        }
    }
}
