using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microservices.FluxoCaixa.Application.Contracts.Persistence;
using Microservices.FluxoCaixa.Application.Mappings;
using Microservices.FluxoCaixa.Application.Messaging;

namespace Microservices.FluxoCaixa.Application.Commands.LancamentoEventStore
{
    public class LancamentoEventStoreCommandHandler : ICommandHandler<LancamentoEventStoreCommand, Guid>
    {
        private readonly IExtratoRepository _extratoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<LancamentoEventStoreCommandHandler> _logger;
        
        public LancamentoEventStoreCommandHandler(IExtratoRepository extratoRepository, IMapper mapper, ILogger<LancamentoEventStoreCommandHandler> logger)
        {
            _extratoRepository = extratoRepository ?? throw new ArgumentNullException(nameof(extratoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Guid> Handle(LancamentoEventStoreCommand request, CancellationToken cancellationToken)
        {

            await _extratoRepository.SalvarLancamento(LancamentoEventStoreCommandToExtratoDtoMap.toDto(request));
            _logger.LogInformation($"Event store atualizada com sucesso.");
            return request.Id;
        }

    }
}
