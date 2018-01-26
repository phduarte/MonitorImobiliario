using System.Collections.Generic;

namespace Gadz.Crawler.Imobiliario.Core.DomainModel.Ofertas {

    internal class OfertaServices {

        readonly IOfertaRepository _repository;

        public OfertaServices(IOfertaRepository ofertaRepository) {
            _repository = ofertaRepository;
        }
        
        internal void Salvar(IOferta oferta) {
            try {
                _repository.Add(oferta);
            } catch {
                throw;
            }
        }

        internal void Salvar(IEnumerable<IOferta> ofertas) {
            _repository.Add(ofertas);
        }

        internal void Limpar() {
            try {
                _repository.Clear();
            } catch {
                throw;
            }
        }

        internal void Limpar(string cidade) {
            _repository.Remove(cidade);
        }

        internal void Finalizar() {

            foreach (var i in _repository.Dump) {
                _repository.Add(i);
            }

            _repository.Undump();
        }
    }
}
