using MediatR;
using Microservices.FluxoCaixa.Application.Messaging;

namespace Microservices.FluxoCaixa.Application.Commands.Sacar
{
    public class SacarContaCorrenteCommand : ICommand<Decimal>
    {
        public Guid ContaCorrenteId { get; set; }
        public decimal ValorTransacao { get; set; }
    }
}
