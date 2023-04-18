namespace Gadz.Crawler.Imobiliario.Domain.Pesquisas;

public class CriteriosDeBusca
{
    public Regiao Regiao { get; set; }
    public decimal PrecoMinimo { get; set; }
    public decimal PrecoMaximo { get; set; }
    public int Dormitorios { get; set; }
    public int Vagas { get; set; }
    public int Banheiros { get; set; }
    public CriterioDeOrdenacao Ordenacao { get; set; } = CriterioDeOrdenacao.DataAtualizacao;
}
