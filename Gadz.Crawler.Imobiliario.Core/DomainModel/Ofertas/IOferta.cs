using System;
using Gadz.Crawler.Imobiliario.Core.DomainModel.Pesquisas;

namespace Gadz.Crawler.Imobiliario.Core.DomainModel.Ofertas {
    public interface IOferta : IEntidade {
        string Anunciante { get; set; }
        string AtualizadoEm { get; set; }
        DateTime DataConsulta { get; set; }
        string Link { get; set; }
        IPesquisa Pesquisa { get; set; }
        string Preco { get; set; }
    }
}