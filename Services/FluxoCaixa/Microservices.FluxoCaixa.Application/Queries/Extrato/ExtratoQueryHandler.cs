using AutoMapper;
using Microservices.FluxoCaixa.Application.Contracts.Persistence;
using Microservices.FluxoCaixa.Application.Dtos;
using Microservices.FluxoCaixa.Application.Messaging;

namespace Microservices.FluxoCaixa.Application.Queries.Extrato
{
    public class ExtratoQueryHandler : IQueryHandler<ExtratoQuery, IEnumerable<ExtratoDto>>
    {
        private readonly IExtratoRepository _extratoRepository;

        public ExtratoQueryHandler(IExtratoRepository extratoRepository, IMapper mapper)
        {
            _extratoRepository = extratoRepository ?? throw new ArgumentNullException(nameof(extratoRepository));
        }

        public async Task<IEnumerable<ExtratoDto>> Handle(ExtratoQuery request, CancellationToken cancellationToken)
        {
           var ret = await _extratoRepository.ObterExtradoPorContaCorrente(request.ContaCorrenteId);
            return ret;
        }

    }
}
