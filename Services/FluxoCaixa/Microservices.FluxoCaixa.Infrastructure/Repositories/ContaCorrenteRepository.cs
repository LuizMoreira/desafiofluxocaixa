using Microservices.FluxoCaixa.Infrastructure.Persistence;
using Microservices.FluxoCaixa.Application.Contracts.Persistence;
using Microservices.FluxoCaixa.Core.Entities.Movimentacao;
using Microsoft.EntityFrameworkCore;

namespace Microservices.FluxoCaixa.Infrastructure.Repositories
{
    public class ContaCorrenteRepository : RepositoryBase<ContaCorrenteRoot>, IContaCorrenteRepository
    {
        public ContaCorrenteRepository(FluxoCaixaContext dbContext) : base(dbContext)
        {
            
        }
        //TODO: DÉBITO TÉCNICO: implementar persistência pessimista
        public async Task<ContaCorrenteRoot> GetContaCorrenteById(Guid id)
        {
            return await _dbContext.ContaCorrente
                                 .Where(o => o.Id == id).Include(a => a.Cliente).FirstOrDefaultAsync();
        }

        public async Task<ContaCorrenteRoot> GetContaCorrenteByNomeClienteAsync(string nome)
        {
            return await _dbContext.ContaCorrente
                                 .Where(o => o.Cliente.Nome == nome).Include(a => a.Cliente).FirstOrDefaultAsync();
        }

    }
}
