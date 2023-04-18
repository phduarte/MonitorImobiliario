namespace Gadz.Crawler.Imobiliario.Domain.Pesquisas
{
    public class Regiao
    {
        public string? Bairro { get; init; }
        public string? Cidade { get; init; }
        public string? Estado { get; init; }
        public string? Zona { get; init; }

        public override string ToString()
        {
            return $"Bairro: {Bairro}, Cidade: {Cidade} - Estado: {Estado}";
        }
    }
}
