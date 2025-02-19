using MediatR;

namespace Microservices.FluxoCaixa.Application.Messaging
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}