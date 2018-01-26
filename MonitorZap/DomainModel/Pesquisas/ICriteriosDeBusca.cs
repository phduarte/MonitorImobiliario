using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Gadz.Crawler.Imobiliario.Core.DomainModel.Pesquisas {
    public interface ICriteriosDeBusca {
        int PrecoMaximo { get; set; }
        IList<string> FiltroDormitorios { get; set; }
        IList<string> FiltroVagas { get; set; }
        Sugestao ParametrosAutosuggest { get; set; }
        int Pagina { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        Ordem Ordem { get; set; }
        string PaginaOrigem { get; }
        string Semente { get; }
        string Formato { get; }
        [JsonIgnore]
        string Cidade { get; set; }
        [JsonIgnore]
        string Estado { get; set; }
    }
}
