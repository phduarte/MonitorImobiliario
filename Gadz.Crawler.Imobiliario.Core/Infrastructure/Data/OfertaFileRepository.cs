using System.IO;
using System.Text;
using System.Collections.Generic;
using Gadz.Crawler.Imobiliario.Core.DomainModel.Ofertas;

namespace Gadz.Crawler.Imobiliario.Core.Infrastructure.Data {

    internal class OfertaFileRepository : IOfertaRepository {

        const string FILE_PATH = "resultados.txt";
        const string SEPARATOR = "\t";
        Encoding CODEPAGE = Encoding.UTF8;

        static OfertaFileRepository _instance;

        public static OfertaFileRepository Instance => _instance = _instance ?? new OfertaFileRepository();

        public IList<IOferta> Dump => throw new System.NotImplementedException();

        public void Add(IOferta resultado) {

            if (!File.Exists(FILE_PATH)) EscreverCabecalho();
                
            using (var arquivo = new StreamWriter(FILE_PATH, true, CODEPAGE)) {

                string[] campos = new string[5];

                campos[0] = resultado.Nome;
                campos[1] = resultado.Preco;
                campos[2] = resultado.Anunciante;
                campos[3] = resultado.AtualizadoEm;
                campos[4] = resultado.Link;

                arquivo.WriteLine(string.Join(SEPARATOR, campos));
            }
        }

        void EscreverCabecalho() {

            using(var arquivo = new StreamWriter(FILE_PATH, false, CODEPAGE)) {

                string[] campos = new string[5];

                campos[0] = "Titulo";
                campos[1] = "Preco";
                campos[2] = "Anunciante";
                campos[3] = "AtualizadoEm";
                campos[4] = "Link";

                arquivo.WriteLine(string.Join(SEPARATOR, campos));
            }
        }

        public void Clear() {
            EscreverCabecalho();
        }

        public void Undump() {
            throw new System.NotImplementedException();
        }

        public void Add(IEnumerable<IOferta> ofertas) {
            throw new System.NotImplementedException();
        }

        public void Remove(string cidade) {
            throw new System.NotImplementedException();
        }
    }
}
