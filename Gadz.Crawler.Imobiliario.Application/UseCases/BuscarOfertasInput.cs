using Gadz.Crawler.Imobiliario.Domain.Pesquisas;

namespace Gadz.Crawler.Imobiliario.Application.UseCases
{
    public class BuscarOfertasInput
    {
        public string? Bairro { get; init; }
        public string? Cidade { get; init; }
        public string? Estado { get; init; }
        public string? Zona { get; init; }
        public decimal PrecoMaximo { get; set; }
        public decimal PrecoMinimo { get; set; }
        public int Dormitorios { get; set; }
        public int Vagas { get; set; }
        public CriterioDeOrdenacao Ordenacao { get; set; } = CriterioDeOrdenacao.DataAtualizacao;

        public CriteriosDeBusca AsCriteriosDeBusca()
        {
            return new()
            {
                Dormitorios = Dormitorios,
                Ordenacao = Ordenacao,
                PrecoMaximo = PrecoMaximo,
                PrecoMinimo = PrecoMinimo,
                Regiao = new()
                {
                    Bairro = Bairro,
                    Cidade = Cidade,
                    Estado = Estado,
                    Zona = Zona
                },
                Vagas = Vagas
            };
        }
    }
}
