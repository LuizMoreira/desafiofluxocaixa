using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microservices.FluxoCaixa.Application.Contracts.Persistence;
using MassTransit;
using Microservices.FluxoCaixa.Core.Common.Enum;
using Microservices.FluxoCaixa.Application.Mappings;
using Microservices.FluxoCaixa.Application.Commands.Depositar;
using Microservices.FluxoCaixa.Application.Messaging;

namespace Microservices.FluxoCaixa.Application.Commands.Sacar
{
    public class SacarContaCorrenteCommandHandler : ICommandHandler<SacarContaCorrenteCommand, decimal>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SacarContaCorrenteCommandHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public SacarContaCorrenteCommandHandler(IContaCorrenteRepository contaCorrenteRepository, IMapper mapper, ILogger<SacarContaCorrenteCommandHandler> logger, IPublishEndpoint publishEndpoint)
        {
            _contaCorrenteRepository = contaCorrenteRepository ?? throw new ArgumentNullException(nameof(contaCorrenteRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        public async Task<decimal> Handle(SacarContaCorrenteCommand request, CancellationToken cancellationToken)
        {
            decimal saldo = 0;
            var contaCorrente = await _contaCorrenteRepository.GetContaCorrenteById(request.ContaCorrenteId);

            if (contaCorrente == null)
            {
                return saldo; 
            }
            contaCorrente.ValorTransacao = request.ValorTransacao;

            contaCorrente.TipoMovimentacao = TipoMovimentacao.Saque;
            contaCorrente.Saldo -= request.ValorTransacao;
            await _contaCorrenteRepository.UpdateAsync(contaCorrente);

            _logger.LogInformation($"Saque efetuado com sucesso.");

            var eventMessage = ContaCorrenteEntityToLancamentoEfetuadoEventMap.toEvent(contaCorrente);
            await _publishEndpoint.Publish(eventMessage);

            return contaCorrente.Saldo;
        }

    }
}
