namespace Gadz.Crawler.Imobiliario.Domain.Pesquisas
{
    public interface IPlataformaDeBusca : IExternalService
    {
        Task<Pesquisa> BuscarOfertas(CriteriosDeBusca pesquisa);
    }
}
