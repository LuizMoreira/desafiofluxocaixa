using AutoFixture;
using AutoMapper;
using Event.Messages.Events;
using MassTransit;
using Microservices.FluxoCaixa.Application.Commands.Depositar;
using Microservices.FluxoCaixa.Application.Contracts.Persistence;
using Microservices.FluxoCaixa.Core.Common.Enum;
using Microservices.FluxoCaixa.Core.Entities.Movimentacao;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Teste.FluxoCaixa.Application
{
    public class DepositarContaCorrenteCommandHandlerTests
    {
        private readonly Mock<IContaCorrenteRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<DepositarContaCorrenteCommandHandler>> _mockLogger;
        private readonly Mock<IPublishEndpoint> _mockPublishEndpoint;
        private readonly DepositarContaCorrenteCommandHandler _handler;
        private readonly DepositarContaCorrenteCommand _command;
        private readonly ContaCorrenteRoot _mockContacorrenteRoot;

        public DepositarContaCorrenteCommandHandlerTests()
        {
            
            _mockRepository = new Mock<IContaCorrenteRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<DepositarContaCorrenteCommandHandler>>();
            _mockPublishEndpoint = new Mock<IPublishEndpoint>();
            _handler = new DepositarContaCorrenteCommandHandler(_mockRepository.Object, _mockMapper.Object, _mockLogger.Object, _mockPublishEndpoint.Object);

            _command = new DepositarContaCorrenteCommand
            {
                ContaCorrenteId = Guid.NewGuid(),
                ValorTransacao = 100
            };
            Fixture fixture = new Fixture();
            _mockContacorrenteRoot = fixture.Create<ContaCorrenteRoot>();
            
        }

        [Fact]
        public async Task Handle_ValidContaCorrente_ReturnsNewSaldo()
        {
            _mockRepository.Setup(repo => repo.GetContaCorrenteById(_command.ContaCorrenteId)).ReturnsAsync(_mockContacorrenteRoot);
            _mockMapper.Setup(mapper => mapper.Map<ContaCorrenteRoot, LancamentoEfetuadoEvent>(_mockContacorrenteRoot)).Returns(new LancamentoEfetuadoEvent());
            var saldoAnterior = _mockContacorrenteRoot.Saldo;
            // Act
            var result = await _handler.Handle(_command, new System.Threading.CancellationToken());

            // Assert
            Assert.Equal(_command.ValorTransacao+ saldoAnterior, result);
            _mockRepository.Verify(repo => repo.UpdateAsync(_mockContacorrenteRoot), Times.Once);
            _mockPublishEndpoint.Verify(endpoint => endpoint.Publish(It.IsAny<LancamentoEfetuadoEvent>(), default), Times.Once);
        }
    }
}
