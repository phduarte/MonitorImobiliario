using Microsoft.Data.Sqlite;
using Gadz.Crawler.Imobiliario.Core.DomainModel.Ofertas;
using System.Collections.Generic;

namespace Gadz.Crawler.Imobiliario.Core.Infrastructure.Data {

    internal class OfertaDatabaseRepository : IOfertaRepository {

        static int _instances = 0;
        string connectionString = @"Data Source=C:\Users\paul_\OneDrive\Documentos\Visual Studio 2017\Projects\MonitorZap\MonitorZap\MonitorZap.db";
        static OfertaDatabaseRepository _instance;
        static volatile int _errosCount = 0;

        public IList<IOferta> Dump { get; private set; }

        public static OfertaDatabaseRepository Instance => _instance = _instance ?? new OfertaDatabaseRepository();

        public OfertaDatabaseRepository() {
            _instances++;
            Dump = new List<IOferta>();
        }

        public void Add(IOferta oferta) {

            using (var conn = new SqliteConnection(connectionString)) {

                conn.Open();

                string sql = @"INSERT INTO Ofertas(Id,Titulo,Anunciante,Link,AtualizadoEm,Preco,DataConsulta,Cidade,Estado) 
                               VALUES(@Id,@Titulo,@Anunciante,@Link,@AtualizadoEm,@Preco,@DataConsulta,@Cidade,@Estado)";

                using (var cmd = new SqliteCommand(sql,conn)) {

                    cmd.Parameters.AddWithValue("@Id", oferta.Id.ToString());
                    cmd.Parameters.AddWithValue("@Titulo", oferta.Nome);
                    cmd.Parameters.AddWithValue("@Anunciante", oferta.Anunciante);
                    cmd.Parameters.AddWithValue("@Link", oferta.Link);
                    cmd.Parameters.AddWithValue("@AtualizadoEm", oferta.AtualizadoEm);
                    cmd.Parameters.AddWithValue("@Preco", oferta.Preco);
                    cmd.Parameters.AddWithValue("@DataConsulta", oferta.DataConsulta.ToShortDateString());
                    cmd.Parameters.AddWithValue("@Cidade", oferta.Pesquisa.Cidade);
                    cmd.Parameters.AddWithValue("@Estado", oferta.Pesquisa.Estado);

                    try {
                        cmd.ExecuteNonQuery();
                        _errosCount = 0;
                    } catch {

                        if (_errosCount > 2) {
                            Dump.Add(oferta);
                            throw;
                        }

                        _errosCount++;

                        Add(oferta);
                    }
                }
                
            }
        }

        public void Clear() {
            using (var conn = new SqliteConnection(connectionString)) {

                conn.Open();

                string sql = @"TRUNCATE TABLE Ofertas";

                using (var cmd = new SqliteCommand(sql, conn)) {
                    try {
                        cmd.ExecuteNonQuery();
                        _errosCount = 0;
                    } catch {

                        if (_errosCount > 2)
                            throw;

                        _errosCount++;

                        Clear();
                    }
                }
            }
        }

        public void Undump() {
            Dump = null;
        }

        public void Add(IEnumerable<IOferta> ofertas) {

            List<string> _linhas = new List<string>();

            foreach(var i in ofertas) {
                _linhas.Add("('"+i.Id.ToString()+"','"+i.Nome+"','"+i.Anunciante+"','"+i.Link+"','"+i.AtualizadoEm+"','"+i.Preco+"','"+i.DataConsulta.ToShortDateString()+"','"+i.Pesquisa.Cidade+"','"+i.Pesquisa.Estado+"')");
            }

            using (var conn = new SqliteConnection(connectionString)) {

                conn.Open();

                string sql = @"INSERT INTO Ofertas(Id,Titulo,Anunciante,Link,AtualizadoEm,Preco,DataConsulta,Cidade,Estado) VALUES" + string.Join(",", _linhas);

                using (var cmd = new SqliteCommand(sql, conn)) {

                    try {
                        cmd.ExecuteNonQuery();
                        _errosCount = 0;
                    } catch {

                        if (_errosCount > 2) {
                            throw;
                        }

                        _errosCount++;
                    }
                }
            }
        }

        public void Remove(string cidade) {
            using (var conn = new SqliteConnection(connectionString)) {

                conn.Open();

                string sql = @"DELETE FROM Ofertas WHERE Cidade = @Cidade";

                using (var cmd = new SqliteCommand(sql, conn)) {

                    cmd.Parameters.AddWithValue("@Cidade", cidade);

                    try {
                        cmd.ExecuteNonQuery();
                        _errosCount = 0;
                    } catch {

                        if (_errosCount > 2)
                            throw;

                        _errosCount++;

                        Clear();
                    }
                }
            }
        }
    }
}
