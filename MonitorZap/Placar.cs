using System;

namespace Gadz.Crawler.Imobiliario.Core {

    public class Placar {

        #region fields

        DateTime _startProcess;
        public static int _gravaçõesEmExecução;
        public static int _páginasEncontradas;
        public static int _buscasRealizadas;
        public static int _buscasEmExecução;
        public static volatile bool _iniciado;
        string _status;

        #endregion

        #region properties
        
        public bool TemBusca => _buscasRealizadas < _páginasEncontradas;
        public string Status { get { return _status; } }
        public DateTime HorarioDeInício => _startProcess;
        public DateTime TempoDecorrido {
            get {
                return new DateTime((DateTime.Now - _startProcess).Ticks);
            }
        }
        public DateTime TempoPrevisto {

            get {
                if (BuscasRealizadas == 0)
                    return new DateTime(0);

                var i = (DateTime.Now - _startProcess).Ticks;
                var x = (i / BuscasRealizadas);
                var r = x * (PáginasEncontradas - BuscasRealizadas);

                if (r < 0)
                    r = 0;

                return new DateTime(r);
            }
        }
        public int Proxima => BuscasRealizadas + 1;
        public bool Iniciado => _iniciado;
        public int BuscasRealizadas => _buscasRealizadas;
        public int BuscasEmExecução => _buscasEmExecução;
        public int GravaçõesEmExecução => _gravaçõesEmExecução;
        public int PáginasEncontradas => _páginasEncontradas;

        #endregion

        public Placar() {
            Iniciar();
        }

        public void Iniciar() {
            _startProcess = DateTime.Now;
            _gravaçõesEmExecução = 0;
            _páginasEncontradas = -1;
            _buscasRealizadas = 0;
            _buscasEmExecução = 0;
            _iniciado = false;
            _status = "Não iniciado";
        }

        public void BuscaBemSucedida() {
            _buscasRealizadas++;
            _iniciado = true;
            _status = "Busca realizada com sucesso.";
        }

        public void BuscaResejtada() {
            //_páginasEncontradas = 0;
            _iniciado = true;
            _status = "Busca realizada sem sucesso.";
        }

        public void BuscaIniciada() {
            ++_buscasEmExecução;
        }

        public void BuscaFinalizada() {
            --_buscasEmExecução;
        }

        public void GravaçãoIniciada() {
            ++_gravaçõesEmExecução;
        }

        public void GravaçãoFinalizada() {
            --_gravaçõesEmExecução;
        }

        public void DefinirBuscasEncontradas(int x) {
            _páginasEncontradas = x;
        }
    }
}
