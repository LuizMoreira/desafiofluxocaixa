using Microservices.FluxoCaixa.Application.Commands.LancamentoEventStore;
using Microservices.FluxoCaixa.Application.Dtos;

namespace Microservices.FluxoCaixa.Application.Mappings
{

    public static class LancamentoEventStoreCommandToExtratoDtoMap
    {
        public static ExtratoDto toDto(this LancamentoEventStoreCommand lancamentoEventStoreCommand)
        {
            return new ExtratoDto()
            {
                Id = lancamentoEventStoreCommand.Id.ToString(),
                IdContaCorrente = lancamentoEventStoreCommand.IdContaCorrente.ToString(),
                Nome = lancamentoEventStoreCommand.Nome,
                TipoMovimentacao = lancamentoEventStoreCommand.TipoMovimentacao,
                Saldo = lancamentoEventStoreCommand.Saldo,
                ValorTransacao = lancamentoEventStoreCommand.ValorTransacao,
                Data = lancamentoEventStoreCommand.Data,
                DataCriacaoEvento = lancamentoEventStoreCommand.DataCriacaoEvento
            };

        }
    }
}
