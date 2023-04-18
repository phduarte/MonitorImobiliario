namespace Gadz.Crawler.Imobiliario.Domain.Ofertas;

public interface IOfertaRepository : IRepository
{
    /// <summary>
    /// Use it to create a new record on database.
    /// </summary>
    /// <param name="oferta"></param>
    Task Add(Oferta oferta);
    /// <summary>
    /// Use it to insert a new range on database.
    /// </summary>
    /// <param name="ofertas"></param>
    Task AddRange(IEnumerable<Oferta> ofertas);
    /// <summary>
    /// Use it to clear all records on database.
    /// </summary>
    Task Clear();
    //IList<Oferta> Dump { get; }
    //void Undump();
    //void Remove(string cidade);
}