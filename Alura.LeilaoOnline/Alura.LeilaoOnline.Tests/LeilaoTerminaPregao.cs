using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1500, new double[] { 800, 700, 1500, 200})]
        [InlineData(1000, new double[] { 200,700, 800, 1000})]
        [InlineData(1000, new double[] { 1000})]
        private static void TestaLeilaoComLancesVariados(double valorEsperado, double [] Lances)
        {
            //Arrange - Cenário
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            leilao.IniciaPregao();

            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            for (int i = 0; i < Lances.Length; i++)
            {
                var lance = Lances[i];

                if (i % 2 == 0)
                {
                    leilao.RecebeLance(fulano, lance);
                }
                else
                {
                    leilao.RecebeLance(maria, lance);
                }
            }

            //Act - Método posto a teste 
            leilao.TerminaPregao();

            //Assert
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }        

        [Fact]
        private static void RetornaZeroDadoUmLeilaoSemlance()
        {
            //Arrange - Cenário
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            leilao.IniciaPregao();

            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);
            var jose = new Interessada("José", leilao);

            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(jose, 1400);
            leilao.RecebeLance(maria, 900);

            //Act - Método posto a teste 
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 1400;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
            Assert.Equal(jose, leilao.Ganhador.Cliente);
        }

        [Fact]
        private void LeilaoSemLance()
        {
            //Arrange - Cenário
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            leilao.IniciaPregao();

            //Act - Método posto a teste 
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);            
        }

        [Fact]
        private void LancaInvalidOperationQuandoTerminaPregaoSemLance()
        {
            //Arrange - Cenário
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);

            var excecaoObtida = Assert.Throws<InvalidOperationException>(
                //Act - Método posto a teste 
                () => leilao.TerminaPregao()
            );

            var mensagemEsperada = "Não é possível terminar o pregao sem o leilao estar em aberto." +
                "Utilize o método FinalizaPregao()";
            Assert.Equal(mensagemEsperada, excecaoObtida.Message);
        }

        [Theory]
        [InlineData(1200,1250, new double[] { 800,1000,1450,1250})]
        public void RetornaValorSuperiorMaisProximo(
            double valorDestino,
            double valorEsperado,
            double[] Lances
        )
        {
            //Arrange - Cenário
            var modalidade = new OfertaSuperiorMaisProxima(valorDestino);

            var leilao = new Leilao("Van Gogh", modalidade);
            leilao.IniciaPregao();

            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            for (int i = 0; i < Lances.Length; i++)
            {
                var lance = Lances[i];

                if (i % 2 == 0)
                {
                    leilao.RecebeLance(fulano, lance);
                }
                else
                {
                    leilao.RecebeLance(maria, lance);
                }
            }

            //Act - Método posto a teste 
            leilao.TerminaPregao();

            //Assert
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);

        }
    }
}
