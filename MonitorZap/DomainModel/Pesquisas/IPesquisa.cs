using System;
using System.Collections.Generic;
using Gadz.Crawler.Imobiliario.Core.DomainModel.Ofertas;

namespace Gadz.Crawler.Imobiliario.Core.DomainModel.Pesquisas {
    public interface IPesquisa : IEntidade, ICriteriosDeBusca, IDisposable {
        bool HasRow { get; }
        IEnumerable<IOferta> Ofertas { get; }
        int Total { get; set; }

        string BaseAddress();
        void NextResult();
        bool Read();
        void Refresh();
    }
}