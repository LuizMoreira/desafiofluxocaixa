using MediatR;
using Microservices.FluxoCaixa.Application.Dtos;
using Microservices.FluxoCaixa.Application.Messaging;

namespace Microservices.FluxoCaixa.Application.Queries.Extrato
{
    public class ExtratoQuery : IQuery<IEnumerable<ExtratoDto>>
    {
        public string ContaCorrenteId { get; set; }
        public DateTime Data { get; set; }
    }
}
