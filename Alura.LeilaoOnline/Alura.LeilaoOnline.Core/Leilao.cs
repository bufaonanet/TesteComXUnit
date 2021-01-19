using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public enum EstadoLeilao
    {
        LeilaoAntesDoPregao,
        Aberto,
        Finalizado
    }

    public class Leilao
    {
        private Interessada _clienteAtual;
        private IList<Lance> _lances;
        public IModalidade Modalidade { get; }
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private  set; }
        public EstadoLeilao EstadoAtual { get; private set; }

        public Leilao(string peca, IModalidade modalidade)
        {
            Peca = peca;
            _lances = new List<Lance>();
            EstadoAtual = EstadoLeilao.LeilaoAntesDoPregao;
            Modalidade = modalidade;            
        }

        private bool LanceAceito(Interessada cliente, double valor)
        {
            return (cliente != _clienteAtual && EstadoAtual == EstadoLeilao.Aberto);
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if (LanceAceito(cliente, valor))
            {
                _lances.Add(new Lance(cliente, valor));
                _clienteAtual = cliente;
            }           
        }

        public void IniciaPregao()
        {
            EstadoAtual = EstadoLeilao.Aberto;
        }

        public void TerminaPregao()
        {
            if(EstadoAtual != EstadoLeilao.Aberto)
            {
                throw new System.InvalidOperationException("Não é possível terminar o pregao sem o leilao estar em aberto." +
                "Utilize o método FinalizaPregao()");
            }

            Ganhador = Modalidade.Avalia(this);                

            EstadoAtual = EstadoLeilao.Finalizado;
        }
    }
}