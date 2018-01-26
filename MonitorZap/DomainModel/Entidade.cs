namespace Gadz.Crawler.Imobiliario.Core.DomainModel {
    public abstract class Entidade : IEntidade {

        public virtual Identidade Id { get; private set; }
        public virtual string Nome { get; set; }

        protected Entidade() {
            Id = Identidade.Criar();
        }

        protected Entidade(Identidade id) {
            Id = id;
        }

        public override string ToString() {
            return Nome;
        }
    }
}
