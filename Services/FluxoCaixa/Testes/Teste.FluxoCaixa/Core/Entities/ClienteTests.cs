using Xunit;
using Microservices.FluxoCaixa.Core.Entities.Movimentacao;

namespace Teste.FluxoCaixa.Core.Entities
{
    public class ClienteTests
    {
        //Create arrange string cpf valido e invalido

        private readonly string cpfValido = "98058600008";
        private readonly string cpfInvalido = "11111111111";



        [Fact]
        public void CriarCliente_ComNomeECpfValidos_DeveCriarNovaInstanciaDeCliente()
        {
            // Arrange
            var nome = "José da Silva";
            var cpfNumero = cpfValido;

            // Act
            var cliente = new Cliente(nome, cpfNumero);

            // Assert
            Assert.Equal(nome, cliente.Nome);
            Assert.NotNull(cliente.CPF);
        }

        [Fact]
        public void EhValido_ClienteComNomeECpfValidos_DeveRetornarTrue()
        {
            // Arrange
            var nome = "José da Silva";
            var cpfNumero = cpfValido;
            var cliente = new Cliente(nome, cpfNumero);

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EhValido_ClienteComNomeEmBranco_DeveRetornarFalse()
        {
            // Arrange
            var nome = "";
            var cpfNumero = cpfValido;
            var cliente = new Cliente(nome, cpfNumero);

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EhValido_ClienteComCpfInvalido_DeveRetornarFalse()
        {
            // Arrange
            var nome = "José da Silva";
            var cpfNumero = cpfInvalido;
            var cliente = new Cliente(nome, cpfNumero);

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.False(result);
        }
    }
}
