using FluentValidation.Results;
using ApplicationException = Microservices.FluxoCaixa.Core.Exceptions.ApplicationException;

namespace Microservices.FluxoCaixa.Application.Exceptions
{
    public sealed class ValidationException : ApplicationException
    {
        public ValidationException(IReadOnlyDictionary<string, string[]> errorsDictionary)
            : base("Erro de validação de negócio", "Ocorreram um ou mais erros")
            => ErrorsDictionary = errorsDictionary;

        public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }
    }
}
