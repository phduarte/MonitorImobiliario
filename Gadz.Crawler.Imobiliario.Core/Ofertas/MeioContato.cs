namespace Gadz.Crawler.Imobiliario.Domain.Ofertas;

public struct MeioContato
{
    public MeioContato(string descricao, string contato)
    {
        Descricao = descricao;
        Contato = contato;
    }

    public string Descricao { get; init; }
    public string Contato { get; init; }
}
