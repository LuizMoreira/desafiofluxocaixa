using AutoFixture;
using AutoMapper;
using Event.Messages.Events;
using MassTransit;
using Microservices.FluxoCaixa.Application.Commands.Sacar;
using Microservices.FluxoCaixa.Application.Contracts.Persistence;
using Microservices.FluxoCaixa.Core.Entities.Movimentacao;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Teste.FluxoCaixa.Application
{
    public class SacarContaCorrenteCommanderHandlerTests
    {
        private readonly Mock<IContaCorrenteRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<SacarContaCorrenteCommandHandler>> _mockLogger;
        private readonly Mock<IPublishEndpoint> _mockPublishEndpoint;
        private readonly SacarContaCorrenteCommandHandler _handler;
        private readonly SacarContaCorrenteCommand _mockCommand;
        private readonly ContaCorrenteRoot _mockContacorrenteRoot;

        public SacarContaCorrenteCommanderHandlerTests()
        {
            
            _mockRepository = new Mock<IContaCorrenteRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<SacarContaCorrenteCommandHandler>>();
            _mockPublishEndpoint = new Mock<IPublishEndpoint>();
            _handler = new SacarContaCorrenteCommandHandler(_mockRepository.Object, _mockMapper.Object, _mockLogger.Object, _mockPublishEndpoint.Object);
            Fixture fixture = new Fixture();
            _mockContacorrenteRoot = fixture.Create<ContaCorrenteRoot>();
            _mockCommand  = fixture.Create<SacarContaCorrenteCommand>();
        }

        [Fact]
        public async Task Handle_ValidContaCorrente_ReturnsNewSaldo()
        {
            _mockRepository.Setup(repo => repo.GetContaCorrenteById(_mockCommand.ContaCorrenteId)).ReturnsAsync(_mockContacorrenteRoot);
            _mockMapper.Setup(mapper => mapper.Map<ContaCorrenteRoot, LancamentoEfetuadoEvent>(_mockContacorrenteRoot)).Returns(new LancamentoEfetuadoEvent());
            var saldoAnterior = _mockContacorrenteRoot.Saldo;
            // Act
            var result = await _handler.Handle(_mockCommand, new System.Threading.CancellationToken());
            var saldoAtual = saldoAnterior - _mockCommand.ValorTransacao;
            // Assert
            Assert.Equal(saldoAtual, result);
            _mockRepository.Verify(repo => repo.UpdateAsync(_mockContacorrenteRoot), Times.Once);
            _mockPublishEndpoint.Verify(endpoint => endpoint.Publish(It.IsAny<LancamentoEfetuadoEvent>(), default), Times.Once);
        }
    }
}
