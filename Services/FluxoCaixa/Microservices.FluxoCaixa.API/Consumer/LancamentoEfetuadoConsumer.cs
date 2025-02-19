using AutoMapper;
using Event.Messages.Events;
using MassTransit;
using MediatR;
using Microservices.FluxoCaixa.Application.Mappings;

namespace Microservices.FluxoCaixa.API.Consumer
{
    public class LancamentoEfetuadoConsumer : IConsumer<LancamentoEfetuadoEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<LancamentoEfetuadoConsumer> _logger;

        public LancamentoEfetuadoConsumer(IMediator mediator, IMapper mapper, ILogger<LancamentoEfetuadoConsumer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<LancamentoEfetuadoEvent> context)
        {
            var command = MovimentacaoContaCorrenteEventToMovimentarEventStoreCommandMap.toCommand(context.Message);
            var result = await _mediator.Send(command);

            _logger.LogInformation("MovimentacaoEfetuadaEvent capturado com sucesso. Depósito Id : {valorMovimentacao}", result);
        }
    }
}
