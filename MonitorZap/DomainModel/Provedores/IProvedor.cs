using Gadz.Crawler.Imobiliario.Core.DomainModel.Pesquisas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gadz.Crawler.Imobiliario.Core.DomainModel.Provedores {
    public interface IProvedor : IEntidade {
        DateTime Data {get;}
        IList<IPesquisa> Pesquisas { get; }
    }
}
