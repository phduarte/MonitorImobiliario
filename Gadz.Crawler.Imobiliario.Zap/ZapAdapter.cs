using Flurl.Http;
using Gadz.Crawler.Imobiliario.Domain.Ofertas;
using Gadz.Crawler.Imobiliario.Domain.Pesquisas;
using HtmlAgilityPack;

namespace Gadz.Crawler.Imobiliario.Zap;

public class ZapAdapter : IPlataformaDeBusca
{
    string baseUrl = "https://www.zapimoveis.com.br";

    public async Task<Pesquisa> BuscarOfertas(CriteriosDeBusca criterios)
    {
        var pesquisa = new Pesquisa();
        var paginaAtual = 1;
        var totalDePaginas = 0;

        ///venda/imoveis/mg+belo-horizonte/2-quartos/?onde=,Minas%20Gerais,Belo%20Horizonte,,,,,city,BR%3EMinas%20Gerais%3ENULL%3EBelo%20Horizonte,-19.831361,-43.984498,&transacao=Venda&tipo=Im%C3%B3vel%20usado&precoMaximo=200000&pagina=1&quartos=2&ordem=Mais%20recente

        do
        {
            //var url = baseUrl
            //    .AppendPathSegment("venda")
            //    .AppendPathSegment("imoveis")
            //    .AppendPathSegment("mg+belo-horizonte")
            //    .AppendPathSegment("2-quartos")
            //    .SetQueryParams(GetArgs(criterios, paginaAtual++));
            var url = new Uri("https://www.zapimoveis.com.br/venda/imoveis/mg+belo-horizonte/2-quartos/?onde=,Minas%20Gerais,Belo%20Horizonte,,,,,city,BR%3EMinas%20Gerais%3ENULL%3EBelo%20Horizonte,-19.831361,-43.984498,&transacao=Venda&tipo=Im%C3%B3vel%20usado&precoMaximo=200000&pagina=1&quartos=2&ordem=Mais%20recente");

            string html = null;

            try
            {
                html = await url.GetStringAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            HtmlDocument document = new();
            document.LoadHtml(html);

            var divs = document.DocumentNode.SelectNodes("//div[@class='card-listing simple-card']");
            var ulPagination = document.DocumentNode.SelectSingleNode("//ul[contains(@class,'pagination__container')]");

            //paginaAtual = ulPagination.GetAttributeValue("current-page", paginaAtual);
            //totalDePaginas = ulPagination.GetAttributeValue("page-count", paginaAtual);

            foreach (var div in divs)
            {
                var preco = div.SelectSingleNode(".//p[contains(@class,'simple-card__price')]");
                var dormitorios = div.SelectSingleNode(".//span[@itemprop='numberOfRooms']");
                var area = div.SelectSingleNode(".//span[@itemprop='floorSize']");
                var banheiros = div.SelectSingleNode(".//span[@itemprop='numberOfBathroomsTotal']");

                var p = preco.InnerText.Replace("\n", string.Empty).Replace(" ", "").Replace("R$","");
                var d = dormitorios.InnerText.Replace("\n", string.Empty).Replace(" ", "");
                var a = area.InnerText.Replace("\n", string.Empty).Replace(" ", "");
                var b = banheiros.InnerText.Replace("\n", string.Empty).Replace(" ", "");

                var oferta = new Oferta
                {
                    PlataformaAnuncio = "Zap Imóveis",
                    Url = url.ToString(),
                    Cidade = criterios.Regiao.Cidade,
                    Estado = criterios.Regiao.Estado,
                    Preco = Convert.ToDecimal(p),

                    Anunciante = new()
                    {
                        Id = Guid.NewGuid(),
                        Nome = "Zap Imóveis",
                        Contatos = new()
                        {
                            new MeioContato("site","www.zapimoveis.com.br"),
                        }
                    },
                };

                pesquisa.Add(oferta);
            }
        } while (paginaAtual < totalDePaginas);

        return pesquisa;
    }

    private static IDictionary<string, object> GetArgs(CriteriosDeBusca pesquisa, int pagina)
    {
        //onde=,Minas%20Gerais,Belo%20Horizonte,,,,,city,BR%3EMinas%20Gerais%3ENULL%3EBelo%20Horizonte,-19.831361,-43.984498,
        //&transacao=Venda
        //&tipo=Im%C3%B3vel%20usado
        //&precoMaximo=200000
        //&pagina=1
        //&quartos=2
        //&ordem=Mais%20recente
        return new Dictionary<string, object>
        {
            //onde = "Minas Gerais, Belo Horizonte",
            { "ordem","Mais recente" },
            {"pagina", pagina },
            { "precoMaximo",pesquisa.PrecoMaximo },
            {"quartos",pesquisa.Dormitorios },
            {"tipo","Imóvel usado" },
            {"transacao","Venda" }
        };
    }
}
