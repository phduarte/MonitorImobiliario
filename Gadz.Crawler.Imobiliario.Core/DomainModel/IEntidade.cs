namespace Gadz.Crawler.Imobiliario.Core.DomainModel {
    public interface IEntidade {
        Identidade Id { get; }
        string Nome { get; set; }
    }
}