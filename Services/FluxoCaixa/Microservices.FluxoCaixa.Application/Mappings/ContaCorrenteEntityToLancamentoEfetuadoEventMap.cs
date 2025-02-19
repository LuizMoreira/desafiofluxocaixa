using Event.Messages.Events;
using Microservices.FluxoCaixa.Core.Common.Enum;
using Microservices.FluxoCaixa.Core.Entities.Movimentacao;

namespace Microservices.FluxoCaixa.Application.Mappings
{

    public static class ContaCorrenteEntityToLancamentoEfetuadoEventMap
    {
        public static LancamentoEfetuadoEvent toEvent(this ContaCorrenteRoot contacorrente)
        {
            return new LancamentoEfetuadoEvent()
            {
                IdContaCorrente = contacorrente.Id,
                Nome = contacorrente.Cliente.Nome,
                TipoMovimentacao = Enum.GetName(typeof(TipoMovimentacao), contacorrente.TipoMovimentacao),
                Saldo = contacorrente.Saldo,
                ValorTransacao = contacorrente.ValorTransacao,
                Data = contacorrente.DataUltimaAlteracao
            };

        }
    }
}
