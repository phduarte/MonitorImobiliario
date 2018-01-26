using System;

namespace Gadz.Crawler.Imobiliario.Core.DomainModel {
    public static class Assertion {

        public static void IsTrue(this bool test) {
            if (!test) throw new Exception($"{nameof(test)} is not true");
        }
    }
}
