using FluentValidation;

namespace Microservices.FluxoCaixa.Application.Commands.Autenticar
{
    public class AutenticarCommandValidator : AbstractValidator<AutenticarCommand>
    {
        public AutenticarCommandValidator()
        {
            RuleFor(p => p.Nome).NotEmpty().WithMessage("Nome é obrigatório");
        }
    }
}
