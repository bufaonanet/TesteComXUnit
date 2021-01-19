using System;
using Alura.LeilaoOnline.Core;

namespace Alura.LeilaoOnline.Console
{
    class Program
    {
        private static void Verifica(double esperado, double obtido)
        {
            var cor = System.Console.ForegroundColor;

            if (esperado == obtido)
            {
                System.Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("Teste Passou!");
            }
            else
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"Teste Falhou. Valor esperado: {esperado}, obtido:{obtido}");
            }

            System.Console.ForegroundColor = cor ;
        }

        private static void TestaLeilaoComUnicoLances()
        {
            //Arrange - Cenário
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);

            var fulano = new Interessada("Fulano", leilao);           

            leilao.RecebeLance(fulano, 800);

            
            //Act - Método posto a teste 
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 800;
            var valorObtido = leilao.Ganhador.Valor;

            Verifica(valorEsperado, valorObtido);           

        }

        private static void TestaLeilaoComVariosLances()
        {
            //Arrange - Cenário
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);

            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(fulano, 1000);
            leilao.RecebeLance(maria, 900);

            //Act - Método posto a teste 
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 1000;
            var valorObtido = leilao.Ganhador.Valor;

            Verifica(valorEsperado, valorObtido);

        }
        static void Main()
        {
            TestaLeilaoComVariosLances();
            TestaLeilaoComUnicoLances();            
        }
    }
}
