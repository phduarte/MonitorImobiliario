namespace Gadz.Crawler.Imobiliario.Domain.Pesquisas
{
    public interface IPesquisasRepository : IPort
    {
        Task Add(Pesquisa pesquisa);
    }
}
