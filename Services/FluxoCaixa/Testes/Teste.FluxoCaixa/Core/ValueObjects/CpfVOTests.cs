using Microservices.FluxoCaixa.Core.Common.ValueObjects;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.FluxoCaixa.Core.ValueObjects
{
    public class CpfVOTests
    {
        [Fact]
        public void CpfVO_Validar_Should_Return_True_When_Cpf_Is_Valid()
        {
            // Arrange
            var cpf = new CpfVO("52998224725");

            // Act
            var result = cpf.Validar();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CpfVO_Validar_Should_Return_False_When_Cpf_Is_Null()
        {
            // Arrange
            var cpf = new CpfVO(null);

            // Act
            var result = cpf.Validar();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CpfVO_Validar_Should_Return_False_When_Cpf_Has_Less_Than_11_Digits()
        {
            // Arrange
            var cpf = new CpfVO("123456789");

            // Act
            var result = cpf.Validar();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CpfVO_Validar_Should_Return_False_When_Cpf_Has_More_Than_11_Digits()
        {
            // Arrange
            var cpf = new CpfVO("123456789123");

            // Act
            var result = cpf.Validar();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CpfVO_Validar_Should_Return_False_When_Cpf_Has_Same_Number_Digits()
        {
            // Arrange
            var cpf = new CpfVO("11111111111");

            // Act
            var result = cpf.Validar();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CpfVO_Validar_Should_Return_False_When_Cpf_Is_Invalid()
        {
            // Arrange
            var cpf = new CpfVO("12345678900");

            // Act
            var result = cpf.Validar();

            // Assert
            Assert.False(result);
        }
    }
}
