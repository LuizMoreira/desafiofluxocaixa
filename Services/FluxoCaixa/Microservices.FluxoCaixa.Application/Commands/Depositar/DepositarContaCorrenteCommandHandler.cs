using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microservices.FluxoCaixa.Application.Contracts.Persistence;
using MassTransit;
using Microservices.FluxoCaixa.Core.Common.Enum;
using Microservices.FluxoCaixa.Application.Mappings;
using Microservices.FluxoCaixa.Application.Messaging;
using System.Net.Http;
using System.Security.Claims;

namespace Microservices.FluxoCaixa.Application.Commands.Depositar
{
    
    public class DepositarContaCorrenteCommandHandler : ICommandHandler<DepositarContaCorrenteCommand, decimal>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DepositarContaCorrenteCommandHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public DepositarContaCorrenteCommandHandler(IContaCorrenteRepository contaCorrenteRepository, IMapper mapper, ILogger<DepositarContaCorrenteCommandHandler> logger, IPublishEndpoint publishEndpoint)
        {
            _contaCorrenteRepository = contaCorrenteRepository ?? throw new ArgumentNullException(nameof(contaCorrenteRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        public async Task<decimal> Handle(DepositarContaCorrenteCommand request, CancellationToken cancellationToken)
        {
            decimal saldo = 0;
            var contaCorrente = await _contaCorrenteRepository.GetContaCorrenteById(request.ContaCorrenteId);

            if (contaCorrente == null)
            {
                return saldo; 
            }
            contaCorrente.ValorTransacao = request.ValorTransacao;

            contaCorrente.TipoMovimentacao = TipoMovimentacao.Deposito;
            contaCorrente.Saldo += request.ValorTransacao;
            await _contaCorrenteRepository.UpdateAsync(contaCorrente);

            _logger.LogInformation($"Depósito efetuado com sucesso.");

            var eventMessage = ContaCorrenteEntityToLancamentoEfetuadoEventMap.toEvent(contaCorrente);
            await _publishEndpoint.Publish(eventMessage);

            return contaCorrente.Saldo;
        }

       

    }
}
