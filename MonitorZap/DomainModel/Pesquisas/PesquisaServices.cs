namespace Gadz.Crawler.Imobiliario.Core.DomainModel.Pesquisas {
    public class PesquisaServices {

        public static IPesquisa Create() {
            return new Pesquisa();
        }

        public static IPesquisa Create(ICriteriosDeBusca criterios) {
            var pesquisa = new Pesquisa(criterios);            
            return pesquisa;
        }
    }
}
