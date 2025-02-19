using Microservices.FluxoCaixa.Application.Dtos;
using MongoDB.Driver;

namespace Microservices.FluxoCaixa.Infrastructure.Persistence
{
    public interface IFluxoCaixaMongoContext
    {
        IMongoCollection<ExtratoDto> Extratos { get; }
    }
}
