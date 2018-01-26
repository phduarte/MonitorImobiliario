using HtmlAgilityPack;
using Gadz.Crawler.Imobiliario.Core.DomainModel.Ofertas;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Gadz.Crawler.Imobiliario.Core.DomainModel.Pesquisas {

    internal class Pesquisa : Entidade, IPesquisa {

        #region fields

        int _pagina = 0;
        int _total = 0;
        int _precoMaximo = 2147483647;
        IEnumerable<IOferta> _ofertas = new List<IOferta>();

        #endregion

        #region properties

        public int PrecoMaximo { get { return _precoMaximo; } set { _precoMaximo = value; } }
        public IList<string> FiltroDormitorios { get; set; }
        public IList<string> FiltroVagas { get; set; }
        public Sugestao ParametrosAutosuggest { get; set; }
        public int Pagina { get { return _pagina; } set { _pagina = value; } }
        public Ordem Ordem { get; set; }
        public string PaginaOrigem { get { return "ResultadoBusca"; } }
        public string Semente { get { return "371368093"; } }
        public string Formato { get { return "Lista"; } }

        public IEnumerable<IOferta> Ofertas { get { return _ofertas; } }
        public string Cidade { get { return ParametrosAutosuggest.Cidade; } set { ParametrosAutosuggest.Cidade = value; } }
        public string Estado { get { return ParametrosAutosuggest.Estado; } set { ParametrosAutosuggest.Estado = value; } }
        public bool HasRow {
            get {
                try {
                    return _ofertas.Count() > 0 && Pagina < Total;
                } catch {
                    return false;
                }
            }
        }

        public int Total {
            get {
                return _total;
            }
            set {
                _total = value;
            }
        }
        
        #endregion

        #region constructors

        public Pesquisa() {
            ParametrosAutosuggest = new Sugestao();
            FiltroDormitorios = new List<string>();
            FiltroVagas = new List<string>();
        }

        public Pesquisa(ICriteriosDeBusca criterios) : this() {
            FiltroDormitorios = criterios.FiltroDormitorios;
            FiltroVagas = criterios.FiltroVagas;
            Ordem = criterios.Ordem;
            Pagina = criterios.Pagina;
            ParametrosAutosuggest = criterios.ParametrosAutosuggest;
            PrecoMaximo = criterios.PrecoMaximo;
        }

        #endregion

        #region methods

        public override string ToString() {
            return $"{ParametrosAutosuggest.Cidade}";
        }

        public void NextResult() {
            _pagina++;
            _ofertas = new List<IOferta>();
        }

        public bool Read() {

            try {

                NextResult();
                Refresh();

                return HasRow;

            } catch {
                return false;
            }
        }

        public void Refresh() {

            try {
                string html = getHtml();
                _ofertas = getOfertas(html);
            } catch {
                throw;
            }
        }

        public string BaseAddress() {

            JsonSerializerSettings settings = new JsonSerializerSettings() {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            string ob = JsonConvert.SerializeObject(this as ICriteriosDeBusca, settings);
            var url = "https://www.zapimoveis.com.br/venda/imoveis/" + Estado + "+" + Cidade.Replace(" ", "-").ToLower() + "/2-quartos/#" + ob;

            return url;
        }

        IList<Oferta> getOfertas(string html) {

            var doc = new HtmlDocument();
            var ofertas = new List<Oferta>();

            doc.LoadHtml(html);

            var total = doc.DocumentNode.SelectNodes("//div[@class='pagination']/span").Single().InnerText.JustNumber();
            var artigos = doc.DocumentNode.SelectNodes("//article[@class='minificha']");

            if (artigos == null)
                return ofertas;

            _total = total;

            //
            foreach (var artigo in artigos) {

                var oferta = new Oferta(this);
                var titulo = artigo.SelectNodes("./section[2]/a/h2/strong").Single().InnerText;
                var anunciante = artigo.SelectNodes("./section[2]/div[1]").Single().InnerText;
                var atualizadoEm = artigo.SelectNodes("./section[2]/em").Single().InnerText;
                var preco = artigo.SelectNodes("./section[1]/a/div[@class='preco']").Single().InnerText;
                var link = artigo.SelectNodes("./section[1]/a").Single().Attributes["href"].Value;

                oferta.Nome = titulo.Decode();
                oferta.Anunciante = anunciante.Decode();
                oferta.AtualizadoEm = atualizadoEm.Decode();
                oferta.Preco = preco.Decode();
                oferta.Link = link.Decode();

                ofertas.Add(oferta);
            }

            return ofertas;
        }

        string getHtml() {

            using (HttpClient client = new HttpClient()) {

                var url = new Uri(BaseAddress()).AbsoluteUri.DecodeUrl();

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

                requestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.84 Safari/537.36");
                requestMessage.Headers.Add("Accept", "text/html");

                var response = client.SendAsync(requestMessage).Result;

                if (!response.IsSuccessStatusCode)
                    throw new InvalidOperationException();

                return response.Content.ReadAsStringAsync().Result;
            }
        }

        #endregion

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                _ofertas = null;

                GC.Collect();

                disposedValue = true;
            }
        }

        //TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~Pesquisa() {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose() {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
