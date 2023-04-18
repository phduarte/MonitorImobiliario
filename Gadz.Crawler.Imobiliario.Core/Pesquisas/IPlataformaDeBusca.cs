namespace Gadz.Crawler.Imobiliario.Domain.Pesquisas
{
    public interface IPlataformaDeBusca : IPort
    {
        Task<Pesquisa> BuscarOfertas(CriteriosDeBusca pesquisa);
    }
}
