namespace Gadz.Crawler.Imobiliario.Core.DomainModel.Pesquisas {
    public class Sugestao {

        public string Bairro { get; set; }
        public string Zona { get; set; }
        public string Cidade { get; set; }
        public string Agrupamento { get; set; }
        public string Estado { get; set; }

        public override string ToString() {
            return $"Bairro: {Bairro}, Cidade: {Cidade} - Estado: {Estado}";
        }
    }
}
