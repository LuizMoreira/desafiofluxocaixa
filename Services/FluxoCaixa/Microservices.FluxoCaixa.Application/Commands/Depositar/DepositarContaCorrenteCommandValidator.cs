using FluentValidation;

namespace Microservices.FluxoCaixa.Application.Commands.Depositar
{
    public class DepositarContaCorrenteCommandCommandValidator : AbstractValidator<DepositarContaCorrenteCommand>
    {
        public DepositarContaCorrenteCommandCommandValidator()
        {
            RuleFor(p => Convert.ToDecimal(p.ValorTransacao)).GreaterThan(0).WithMessage("Valor transação é obrigatório");
        }
    }
}
