using Microservices.FluxoCaixa.Core.Common;
using Microservices.FluxoCaixa.Core.Common.Enum;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Microservices.FluxoCaixa.Core.Entities.Movimentacao
{
    public class ContaCorrenteRoot : EntityBase
    {
        
        public Cliente Cliente { get; set; }
        public decimal Saldo { get; set; }
        public TipoMovimentacao TipoMovimentacao { get; set; }
        public decimal ValorTransacao { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }


        public ContaCorrenteRoot() { }

        public ContaCorrenteRoot(string nome, string cpf, decimal saldo, TipoMovimentacao tipoMovimentacao, decimal valorTransacao)
        {
            Cliente = new Cliente(nome, cpf);
            Saldo = saldo;
            TipoMovimentacao = tipoMovimentacao;
            ValorTransacao = valorTransacao;

        }
    }
}
