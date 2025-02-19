using Microservices.FluxoCaixa.Core.Common;
using Microservices.FluxoCaixa.Core.Common.ValueObjects;

namespace Microservices.FluxoCaixa.Core.Entities.Movimentacao
{
    public class Cliente : EntityBase
    {
        public string Nome { get; private set; }
        public CpfVO CPF { get; private set; }

        public Cliente(string nome, string cpfNumero)
        {
            Id = Guid.NewGuid();
            Nome = nome;

            this.AtribuirCpf(cpfNumero);

            this.EhValido();

        }

        // Para o EF
        protected Cliente() { }

        private bool AtribuirCpf(string cpfNumero)
        {
            var cpf = new CpfVO(cpfNumero);
            if (!cpf.Validar()) return false;

            CPF = cpf;
            return true;
        }

        public bool EhValido()
        {
            if (string.IsNullOrEmpty(Nome))
                return false;

            if (CPF==null || !CPF.Validar())
                return false;

            return true;
        }
    }
}


