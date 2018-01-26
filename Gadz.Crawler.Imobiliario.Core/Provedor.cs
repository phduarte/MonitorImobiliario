using Gadz.Crawler.Imobiliario.Core.DomainModel.Ofertas;
using Gadz.Crawler.Imobiliario.Core.DomainModel.Pesquisas;
using Gadz.Crawler.Imobiliario.Core.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Gadz.Crawler.Imobiliario.Core {

    #region delegates

    public delegate void OfertaFound(string message);
    public delegate void ErrorOcurred(string message);
    public delegate void RefreshCompleted(Placar status);
    public delegate void Completed(Placar i);

    #endregion

    public class Provedor {

        #region fields

        static Provedor _instance;
        readonly OfertaServices _ofertaServices;

        #endregion

        #region properties

        Placar placar = new Placar();

        #endregion

        #region events

        public event OfertaFound OnOfertaFound;
        public event ErrorOcurred OnErrorOccurred;
        public event RefreshCompleted OnRefreshCompleted;
        public event Completed OnCompleted;

        #endregion

        #region constructors

        public Provedor() {
            _ofertaServices = new OfertaServices(OfertaDatabaseRepository.Instance);
        }

        #endregion

        public static Provedor Instance => _instance = _instance ?? new Provedor();

        public ICriteriosDeBusca CriarCriteriosDePesquisa() {
            return PesquisaServices.Create();
        }

        public void Pesquisar(ICriteriosDeBusca criterios) {

            placar.Iniciar();

            _ofertaServices.Limpar(criterios.Cidade);

            do {

                if (placar.BuscasEmExecução >= Configurações.MáximoBuscasSimultâneas)
                    continue;

                BuscarPróximaPáginaFaltante(criterios);

                while (!placar.Iniciado) Thread.Sleep(500);

            } while (placar.TemBusca);

            EsperarAtéQue(placar.GravaçõesEmExecução == 0);

            OnCompleted?.Invoke(placar);
        }

        void BuscarPróximaPáginaFaltante(ICriteriosDeBusca criterios) {

            new Thread(() => {

                BuscarAsync(criterios);

            }) { IsBackground = true }
            .Start();
        }

        void BuscarAsync(ICriteriosDeBusca criterios) {

            Debug.WriteLine("{0:HH:mm:ss} - BuscarAsync começou", DateTime.Now);

            if (placar.BuscasEmExecução >= Configurações.MáximoBuscasSimultâneas)
                return;

            placar.BuscaIniciada();

            using (var pesquisa = PesquisaServices.Create()) {

                pesquisa.Cidade = criterios.Cidade;
                pesquisa.Estado = criterios.Estado;
                pesquisa.FiltroDormitorios = criterios.FiltroDormitorios;
                pesquisa.Ordem = criterios.Ordem;
                pesquisa.FiltroVagas = criterios.FiltroVagas;
                pesquisa.Pagina = placar.Proxima;

                if (pesquisa.Read()) {

                    placar.DefinirBuscasEncontradas(pesquisa.Total);
                    placar.BuscaBemSucedida();

                    SalvarOfertasAsync(pesquisa.Ofertas);

                    OnRefreshCompleted?.Invoke(placar);

                } else {

                    placar.BuscaResejtada();
                }
            }

            placar.BuscaFinalizada();

            Debug.WriteLine("{0:HH:mm:ss} - BuscarAsync saiu", DateTime.Now);
        }

        void SalvarOfertasAsync(IEnumerable<IOferta> ofetas) {

            Debug.WriteLine("{0:HH:mm:ss} - SalvarAsync começou", DateTime.Now);

            new Thread(() => {

                //EsperarAtéQue(placar.GravaçõesEmExecução < Configurações.MáximoGravaçõesSimultâneas);
                while (placar.GravaçõesEmExecução >= Configurações.MáximoGravaçõesSimultâneas)
                    Thread.Sleep(500);

                placar.GravaçãoIniciada();

                _ofertaServices.Salvar(ofetas);

                foreach (var oferta in ofetas) {

                    try {
                        OnOfertaFound?.Invoke(oferta.ToString());

                    } catch (Exception ex) {
                        OnErrorOccurred?.Invoke(ex.Message);
                    }
                }

                placar.GravaçãoFinalizada();

            }).Start();

            Debug.WriteLine("{0:HH:mm:ss} - SalvarAsync saiu", DateTime.Now);
        }

        void EsperarAtéQue(bool condição) {
            Debug.WriteLine("{0:HH:mm:ss} - EsperarAte começou", DateTime.Now);
            while (!condição) Thread.Sleep(100);
            Debug.WriteLine("{0:HH:mm:ss} - EsperarAte saiu", DateTime.Now);
        }
    }
}
