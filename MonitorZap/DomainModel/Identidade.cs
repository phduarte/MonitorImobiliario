using System;

namespace Gadz.Crawler.Imobiliario.Core.DomainModel {

    public struct Identidade {

        string _id ;

        public Identidade(string id) {
            _id = id;
        }

        public static Identidade Criar() {
            return new Identidade(Guid.NewGuid().ToString());
        }

        public override string ToString() {
            return _id;
        }
    }
}
