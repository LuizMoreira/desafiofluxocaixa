using AutoMapper;
using MassTransit;
using Microservices.FluxoCaixa.Application.Commands.Depositar;
using Microservices.FluxoCaixa.Application.Contracts.Persistence;
using Microservices.FluxoCaixa.Application.Mappings;
using Microservices.FluxoCaixa.Application.Messaging;
using Microservices.FluxoCaixa.Core.Common.Enum;
using Microsoft.Extensions.Logging;

namespace Microservices.FluxoCaixa.Application.Commands.Autenticar
{
    public class AutenticarCommandHandler : ICommandHandler<AutenticarCommand, AutenticacaoRespose>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AutenticarCommandHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public AutenticarCommandHandler(IContaCorrenteRepository contaCorrenteRepository, IMapper mapper, ILogger<AutenticarCommandHandler> logger)
        {
            _contaCorrenteRepository = contaCorrenteRepository ?? throw new ArgumentNullException(nameof(contaCorrenteRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<AutenticacaoRespose> Handle(AutenticarCommand request, CancellationToken cancellationToken)
        {
            var contaCorrente = await _contaCorrenteRepository.GetContaCorrenteByNomeClienteAsync(request.Nome);

            return contaCorrente is null ? null : new AutenticacaoRespose()
            {
                ContaCorrenteId = contaCorrente.Id,
                Nome = contaCorrente?.Cliente?.Nome,
            };
        }

    }
}


