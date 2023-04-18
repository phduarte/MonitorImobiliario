namespace Gadz.Crawler.Imobiliario.Domain.Ofertas;

public class Oferta : Entidade
{
    private readonly List<string> _fotos = new();

    public string Titulo { get; init; }
    public string Descricao { get; set; }
    public decimal Preco { get; init; }
    public decimal Area { get; set; }
    public int Andar { get; set; } = 1;
    public TipoImovel Tipo { get; set; } = TipoImovel.NaoIdentificado;
    public Anunciante Anunciante { get; init; }
    public DateTime AtualizadoEm { get; init; } = DateTime.Now;
    public string PlataformaAnuncio { get; set; }
    public string Url { get; init; }
    public IReadOnlyList<string> Fotos => _fotos;
    public DateTime DataConsulta { get; init; } = DateTime.Now;
    public string Bairro { get; init; }
    public string Cidade { get; init; }
    public string Estado { get; init; }

    public void AdicionarFoto(string foto)
    {
        _fotos.Add(foto);
    }

    public override string ToString()
    {
        return $"{Titulo} - {Preco} - {Anunciante}";
    }
}

public enum TipoImovel
{
    NaoIdentificado,
    Casa,
    Apartamento,
    Loja,
    Chacara
}
