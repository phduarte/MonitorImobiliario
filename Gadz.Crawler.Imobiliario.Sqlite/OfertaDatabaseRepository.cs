using Gadz.Crawler.Imobiliario.Domain.Ofertas;
using Microsoft.Data.Sqlite;
using System.Diagnostics.CodeAnalysis;

namespace Gadz.Crawler.Imobiliario.Sqlite;

public class OfertaDatabaseRepository : IOfertaRepository
{
    static volatile int _errosCount = 0;
    private readonly string _connectionString;

    public IList<Oferta> Dump { get; private set; }

    public OfertaDatabaseRepository(string connectionString)
    {
        _connectionString = connectionString;
        Dump = new List<Oferta>();
    }

    public async Task Add(Oferta oferta)
    {
        string sql = @"INSERT INTO Ofertas(Id,Titulo,Anunciante,Link,AtualizadoEm,Preco,DataConsulta,Cidade,Estado) 
                       VALUES(@Id,@Titulo,@Anunciante,@Link,@AtualizadoEm,@Preco,@DataConsulta,@Cidade,@Estado)";

        using var cmd = CreateOpenConnection().CreateCommand();

        cmd.CommandText = sql;
        cmd.Parameters.AddWithValue("@Id", oferta.Id.ToString());
        cmd.Parameters.AddWithValue("@Titulo", oferta.Titulo);
        cmd.Parameters.AddWithValue("@Anunciante", oferta.Anunciante);
        cmd.Parameters.AddWithValue("@Link", oferta.Url);
        cmd.Parameters.AddWithValue("@AtualizadoEm", oferta.AtualizadoEm);
        cmd.Parameters.AddWithValue("@Preco", oferta.Preco);
        cmd.Parameters.AddWithValue("@DataConsulta", oferta.DataConsulta.ToShortDateString());
        cmd.Parameters.AddWithValue("@Cidade", oferta.Cidade);
        cmd.Parameters.AddWithValue("@Estado", oferta.Estado);

        try
        {
            await cmd.ExecuteNonQueryAsync();
            _errosCount = 0;
        }
        catch
        {

            if (_errosCount > 2)
            {
                Dump.Add(oferta);
                throw;
            }

            _errosCount++;

            await Add(oferta);
        }
    }

    public async Task Clear()
    {
        string sql = @"TRUNCATE TABLE Ofertas";

        using var cmd = CreateOpenConnection().CreateCommand();

        try
        {
            cmd.CommandText = sql;
            await cmd.ExecuteNonQueryAsync();
            _errosCount = 0;
        }
        catch
        {

            if (_errosCount > 2)
                throw;

            _errosCount++;

            await Clear();
        }
    }

    public async Task AddRange(IEnumerable<Oferta> ofertas)
    {
        List<string> _linhas = new();

        foreach (var i in ofertas)
        {
            _linhas.Add("('" + i.Id.ToString() + "','" + i.Titulo + "','" + i.Anunciante + "','" + i.Url + "','" + i.AtualizadoEm + "','" + i.Preco + "','" + i.DataConsulta.ToShortDateString() + "','" + i.Cidade + "','" + i.Estado + "')");
        }

        string sql = @"INSERT INTO Ofertas(Id,Titulo,Anunciante,Link,AtualizadoEm,Preco,DataConsulta,Cidade,Estado) VALUES" + string.Join(",", _linhas);

        using var cmd = CreateOpenConnection().CreateCommand();

        try
        {
            cmd.CommandText = sql;
            await cmd.ExecuteNonQueryAsync();
            _errosCount = 0;
        }
        catch
        {
            if (_errosCount > 2)
            {
                throw;
            }

            _errosCount++;
        }
    }

    public void Remove(string cidade)
    {
        string sql = @"DELETE FROM Ofertas WHERE Cidade = @Cidade";

        using var cmd = CreateOpenConnection().CreateCommand();

        try
        {
            cmd.Parameters.AddWithValue("@Cidade", cidade);
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();

            _errosCount = 0;
        }
        catch
        {

            if (_errosCount > 2)
                throw;

            _errosCount++;

            Clear();
        }
    }

    private SqliteConnection CreateOpenConnection()
    {
        using var cnn = new SqliteConnection(_connectionString);
        cnn.Open();
        return cnn;
    }
}
