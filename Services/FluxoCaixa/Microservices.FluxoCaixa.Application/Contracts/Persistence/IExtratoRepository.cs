using Microservices.FluxoCaixa.Application.Dtos;

namespace Microservices.FluxoCaixa.Application.Contracts.Persistence
{
    public interface IExtratoRepository
    {

        Task SalvarLancamento(ExtratoDto extrato);
        Task<IEnumerable<ExtratoDto>> ObterExtradoPorContaCorrente(string idContaCorrente);
        Task<IEnumerable<ExtratoDto>> ObterExtratoPorContaCorrenteDiaAsync(string idContaCorrente, DateTime date);


    }
}
