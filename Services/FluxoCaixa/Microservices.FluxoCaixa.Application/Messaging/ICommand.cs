using MediatR;

namespace Microservices.FluxoCaixa.Application.Messaging
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}