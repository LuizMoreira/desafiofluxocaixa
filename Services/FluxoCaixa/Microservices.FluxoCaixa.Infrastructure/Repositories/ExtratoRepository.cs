using Microservices.FluxoCaixa.Application.Contracts.Persistence;
using Microservices.FluxoCaixa.Application.Dtos;
using Microservices.FluxoCaixa.Infrastructure.Persistence;
using MongoDB.Driver;

namespace Microservico.Separacao.Infrastructure.Repositories
{
    public class ExtratoRepository : IExtratoRepository
    {
        private readonly IFluxoCaixaMongoContext _context;
        public ExtratoRepository(IFluxoCaixaMongoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task SalvarLancamento(ExtratoDto extrato)
        {
            await _context.Extratos.InsertOneAsync(extrato);
        }

        public async Task<IEnumerable<ExtratoDto>> ObterExtradoPorContaCorrente(string idContaCorrente)
        {
                FilterDefinition<ExtratoDto> filter = Builders<ExtratoDto>.Filter.Eq(p => p.IdContaCorrente, idContaCorrente.ToLower());
                var ret =   await _context
                                .Extratos
                                .Find(filter)
                                .ToListAsync();
                return ret?.OrderBy(a=>a.Data);

        }


        public async Task<IEnumerable<ExtratoDto>> ObterExtratoPorContaCorrenteDiaAsync(string idContaCorrente, DateTime date)
        {
            var filter = Builders<ExtratoDto>.Filter.And(
                Builders<ExtratoDto>.Filter.Eq(p => p.IdContaCorrente, idContaCorrente),
                Builders<ExtratoDto>.Filter.Gte(p => p.Data, date.Date),
                Builders<ExtratoDto>.Filter.Lt(p => p.Data, date.Date.AddDays(1))
            );

            return await _context.Extratos.Find(filter).ToListAsync();
        }

    }
}
