using MediatR;
using Microservices.FluxoCaixa.Application.Messaging;
using Microservices.FluxoCaixa.Core.Common.Enum;

namespace Microservices.FluxoCaixa.Application.Commands.LancamentoEventStore
{
    public class LancamentoEventStoreCommand : ICommand<Guid>
    {
        public Guid Id { get; set; }
        public Guid IdContaCorrente { get; set; }
        public string Nome { get; set; }
        public string TipoMovimentacao { get; set; }
        public decimal Saldo { get; set; }
        public decimal ValorTransacao { get; set; }
        public DateTime? Data { get; set; }
        public DateTime DataCriacaoEvento { get; set; }
    }
}
