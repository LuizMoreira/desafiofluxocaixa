using Microservices.FluxoCaixa.Core.Entities.Movimentacao;

namespace Microservices.FluxoCaixa.Application.Contracts.Persistence
{
    public interface IContaCorrenteRepository : IAsyncRepository<ContaCorrenteRoot>
    {
        Task<ContaCorrenteRoot> GetContaCorrenteById(Guid id);
        Task<ContaCorrenteRoot> GetContaCorrenteByNomeClienteAsync(string nome);

    }
}
