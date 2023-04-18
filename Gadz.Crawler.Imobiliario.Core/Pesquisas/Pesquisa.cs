using Gadz.Crawler.Imobiliario.Domain.Ofertas;

namespace Gadz.Crawler.Imobiliario.Domain.Pesquisas;

public class Pesquisa : Entidade
{
    private readonly List<Oferta> _ofertas = new();

    public DateTime DataPesquisa { get; set; }
    public CriteriosDeBusca CriteriosDeBusca { get; set; } = new CriteriosDeBusca();
    public IReadOnlyList<Oferta> Ofertas => _ofertas;

    public void Add(Oferta o)
    {
        _ofertas.Add(o);
    }

    public override string ToString()
    {
        return $"{CriteriosDeBusca.Regiao.Cidade}";
    }
}
