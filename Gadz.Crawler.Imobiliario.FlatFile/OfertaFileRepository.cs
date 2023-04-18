using Gadz.Crawler.Imobiliario.Domain.Ofertas;
using Gadz.Crawler.Imobiliario.Domain.Pesquisas;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Gadz.Crawler.Imobiliario.FlatFile;

internal class OfertaFileRepository : IOfertaRepository, IPesquisasRepository
{

    const string FILE_PATH = "resultados.txt";
    const string SEPARATOR = "\t";
    readonly Encoding CODEPAGE = Encoding.UTF8;

    static OfertaFileRepository? _instance;

    public static IOfertaRepository Instance => _instance ??= new OfertaFileRepository();

    public IList<Oferta> Dump => throw new NotImplementedException();

    public async Task Add(Oferta resultado)
    {
        if (!File.Exists(FILE_PATH))
        {
            await WriteHeader();
        }

        using var arquivo = new StreamWriter(FILE_PATH, true, CODEPAGE);

        string[] campos = new string[5];

        campos[0] = resultado.Titulo;
        campos[1] = resultado.Preco.ToString();
        campos[2] = resultado.Anunciante.Nome;
        campos[3] = resultado.DataConsulta.ToString("yyyy-MM-dd HH:mm:ss.fff");
        campos[4] = resultado.Url;

        await arquivo.WriteLineAsync(string.Join(SEPARATOR, campos));
    }

    public async Task Clear()
    {
        await WriteHeader();
    }

    public async Task AddRange(IEnumerable<Oferta> ofertas)
    {
        foreach (var o in ofertas)
        {
            await Add(o);
        }
    }

    private async Task WriteHeader()
    {
        using var arquivo = new StreamWriter(FILE_PATH, false, CODEPAGE);

        string[] campos = new string[5];

        campos[0] = "Titulo";
        campos[1] = "Preco";
        campos[2] = "Anunciante";
        campos[3] = "AtualizadoEm";
        campos[4] = "Link";

        await arquivo.WriteLineAsync(string.Join(SEPARATOR, campos));
    }

    public async Task Add(Pesquisa pesquisa)
    {
        //
    }
}
