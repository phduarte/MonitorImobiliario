namespace Gadz.Crawler.Imobiliario.Core {

    public static class Configurações {

        static int maxBuscas = 3;
        static int maxGravacoes = 10;

        public static int MáximoBuscasSimultâneas { get { return maxBuscas; } set { maxBuscas = value; } }
        public static int MáximoGravaçõesSimultâneas { get { return maxGravacoes; } set { maxGravacoes = value; } }
    }
}
