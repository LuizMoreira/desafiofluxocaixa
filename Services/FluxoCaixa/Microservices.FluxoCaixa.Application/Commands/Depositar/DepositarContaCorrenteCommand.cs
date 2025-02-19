using MediatR;
using Microservices.FluxoCaixa.Application.Messaging;
using Microservices.FluxoCaixa.Core.Common.Enum;

namespace Microservices.FluxoCaixa.Application.Commands.Depositar
{
    public class DepositarContaCorrenteCommand : ICommand<Decimal>
    {
        public Guid ContaCorrenteId { get; set; }
        public decimal ValorTransacao { get; set; }
    }
}
