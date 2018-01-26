using Gadz.Crawler.Imobiliario.Core.DomainModel.Pesquisas;
using System;

namespace Gadz.Crawler.Imobiliario.Core.DomainModel.Ofertas {

    internal class Oferta : Entidade, IOferta {

        public string Preco { get; set; }
        public string Anunciante { get; set; }
        public string AtualizadoEm { get; set; }
        public string Link { get; set; }
        public DateTime DataConsulta { get; set; }
        public IPesquisa Pesquisa { get; set; }

        private Oferta() {
            DataConsulta = DateTime.Now;
        }

        public Oferta(Pesquisa pesquisa) : this() {
            Pesquisa = pesquisa;
        }

        public Oferta(Identidade id) : base(id) {

        }

        public override string ToString() {
            return $"{Nome} - {Preco} - {Anunciante}";
        }
    }
}
