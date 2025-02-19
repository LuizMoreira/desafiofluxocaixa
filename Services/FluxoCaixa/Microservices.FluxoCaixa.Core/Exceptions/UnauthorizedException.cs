namespace Microservices.FluxoCaixa.Core.Exceptions
{
    public abstract class UnauthorizedException : ApplicationException
    {
        protected UnauthorizedException(string message)
            : base("Unauthorized", message)
        {
        }
    }
}
