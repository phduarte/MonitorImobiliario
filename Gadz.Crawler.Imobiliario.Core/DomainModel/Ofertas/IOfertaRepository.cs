using System.Collections.Generic;

namespace Gadz.Crawler.Imobiliario.Core.DomainModel.Ofertas {
    public interface IOfertaRepository {
        /// <summary>
        /// Use it to create a new record on database.
        /// </summary>
        /// <param name="oferta"></param>
        void Add(IOferta oferta);
        /// <summary>
        /// Use it to insert a new range on database.
        /// </summary>
        /// <param name="ofertas"></param>
        void Add(IEnumerable<IOferta> ofertas);
        /// <summary>
        /// Use it to clear all records on database.
        /// </summary>
        void Clear();
        IList<IOferta> Dump { get; }
        void Undump();
        void Remove(string cidade);
    }
}