using Gadz.Crawler.Imobiliario.Core;
using Gadz.Crawler.Imobiliario.Core.DomainModel.Pesquisas;
using System;

namespace Gadz.Crawler.Imobiliario {

    class Program {

        static object _lock = new object();

        static void Main(string[] args) {

            var provedor = Provedor.Instance;

            do {

                RedefinirTela();

                string estado = PegarEstado();
                string cidade = PegarCidade(estado);

                Console.Clear();

                var criterios = provedor.CriarCriteriosDePesquisa();

                provedor.OnOfertaFound += Escrever;
                provedor.OnErrorOccurred += EscreverErro;
                provedor.OnRefreshCompleted += AtualizarStatus;
                provedor.OnCompleted += Concluir;

                criterios.FiltroDormitorios.Add("2");
                criterios.FiltroVagas.Add("1");
                criterios.ParametrosAutosuggest.Cidade = cidade;
                criterios.ParametrosAutosuggest.Estado = estado;
                criterios.Ordem = Ordem.Valor;

                provedor.Pesquisar(criterios);

            } while (true);
        }

        private static void RedefinirTela() {
            Console.Title = "Crawler de Imóveis";
            Console.Clear();
        }

        private static string PegarCidade(string estado) {
            Console.Write("Em qual cidade de " + estado + " você deseja buscar? ");
            return Console.ReadLine();
        }

        private static string PegarEstado() {
            Console.Write("Em qual estado você deseja buscar? ");
            return Console.ReadLine();
        }

        static void AtualizarStatus(Placar status) {
            float percentual = status.PáginasEncontradas > 0 ? status.BuscasRealizadas / (float)status.PáginasEncontradas : 0F;
            string _ = string.Format("Tempo total decorrido {0:HH:mm:ss} . Tempo restante previsto: {1:HH:mm:ss}", status.TempoDecorrido, status.TempoPrevisto);
            Console.Title = string.Format("Página {0} de {1} ({5:P0})- {2} processos simultâneos e {3} gravações paralelas - {4}", status.BuscasRealizadas, status.PáginasEncontradas, status.BuscasEmExecução, status.GravaçõesEmExecução, _, percentual);
        }

        static void Concluir(Placar i) {
            Console.Title = "Monitor Zap... Obrigado por ter aguardado até o final!";

            Log(string.Format("{0} páginas consultadas em {1:HH:mm:ss}", i.BuscasRealizadas, i.TempoDecorrido), ConsoleColor.Green);
            Console.WriteLine();
            Log("Pressione qualquer tecla para continuar.", ConsoleColor.Cyan);

            Console.ReadKey();
        }

        static void Escrever(string message) {
            Log(message, ConsoleColor.White);
        }

        static void EscreverErro(string erro) {
            Log(erro, ConsoleColor.Red);
        }

        static void Log(string text, ConsoleColor color) {
            lock (_lock) {
                var _f = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.WriteLine(string.Format("{0:HH:mm:ss} - {1}", DateTime.Now, text));
                Console.ForegroundColor = _f;
            }
        }
    }
}
