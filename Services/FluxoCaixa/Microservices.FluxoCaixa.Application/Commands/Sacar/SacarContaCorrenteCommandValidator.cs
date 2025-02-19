using FluentValidation;

namespace Microservices.FluxoCaixa.Application.Commands.Sacar
{
    public class SacarContaCorrenteCommandValidator : AbstractValidator<SacarContaCorrenteCommand>
    {
        public SacarContaCorrenteCommandValidator()
        {
            RuleFor(p => Convert.ToDecimal(p.ValorTransacao)).GreaterThan(0).WithMessage("Valor transação é obrigatório");
        }
    }
}
