using System;
using Xunit;
using Alura.LeilaoOnline.Core;
using System.Linq;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeLance
    {
        [Fact]
        public void NaoPermiteLancesConsecutivosDoMesmoCliente()
        {
            //Arrange - Cenário
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);

            leilao.IniciaPregao();

            var fulano = new Interessada("Fulano", leilao);

            leilao.RecebeLance(fulano, 500);

            //Act - Método posto a teste 
            leilao.RecebeLance(fulano, 800);

            //Assert           
            var valorObtido = leilao.Lances.Count();
            var qtdEsperada = 1;

            Assert.Equal(qtdEsperada, valorObtido);
        }

        [Theory]
        [InlineData(1, new double[] { 900})]
        [InlineData(2, new double[] { 900, 500})]
        [InlineData(4, new double[] { 900, 500, 50, 60})]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int qtdEsperada, double[] Lances)
        {
            //Arrange - Cenário
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);

            leilao.IniciaPregao();

            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            for (int i = 0; i < Lances.Length ; i++)
            {
                var lance = Lances[i];

                if(i % 2 == 0)
                {
                    leilao.RecebeLance(fulano, lance);
                }
                else
                {
                    leilao.RecebeLance(maria, lance);
                }
            }           

            leilao.TerminaPregao();

            //Act - Método posto a teste 
            leilao.RecebeLance(fulano, 1400);      

            //Assert           
            var valorObtido = leilao.Lances.Count();

            Assert.Equal(qtdEsperada, valorObtido);
        }
    }
}
