using Event.Messages.Events;
using Microservices.FluxoCaixa.Application.Commands.LancamentoEventStore;

namespace Microservices.FluxoCaixa.Application.Mappings
{

    public static class MovimentacaoContaCorrenteEventToMovimentarEventStoreCommandMap
    {
        public static LancamentoEventStoreCommand toCommand(this LancamentoEfetuadoEvent movimentacaoEfetuada)
        {
            return new LancamentoEventStoreCommand()
            {
                Id = movimentacaoEfetuada.Id,
                IdContaCorrente = movimentacaoEfetuada.IdContaCorrente,
                Nome = movimentacaoEfetuada.Nome,
                TipoMovimentacao = movimentacaoEfetuada.TipoMovimentacao,
                Saldo = movimentacaoEfetuada.Saldo,
                ValorTransacao = movimentacaoEfetuada.ValorTransacao,
                Data = movimentacaoEfetuada.Data,
                DataCriacaoEvento = movimentacaoEfetuada.DataCriacaoEvento
            };

        }
    }
}
