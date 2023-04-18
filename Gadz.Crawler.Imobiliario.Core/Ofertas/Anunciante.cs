namespace Gadz.Crawler.Imobiliario.Domain.Ofertas;

public class Anunciante : Entidade
{
    public string Nome { get; init; }
    public List<MeioContato> Contatos { get; set; } = new();
}
