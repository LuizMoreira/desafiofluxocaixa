using Microservices.FluxoCaixa.Core.Entities.Movimentacao;
using Microsoft.Extensions.Logging;

namespace Microservices.FluxoCaixa.Infrastructure.Persistence
{
    public class FluxoCaixaContextSeed
    {
        public static async Task SeedAsync(FluxoCaixaContext fluxoCaixaContext, ILogger<FluxoCaixaContextSeed> logger)
        {            
            if (!fluxoCaixaContext.Cliente.Any())
            {
                fluxoCaixaContext.ContaCorrente.Add(ObterContaCorrentePreConfigurada());
                await fluxoCaixaContext.SaveChangesAsync();
                logger.LogInformation("Semeando bando de dados associado ao contexto {DbContextName}", typeof(FluxoCaixaContext).Name);
            }
        }

        private static ContaCorrenteRoot ObterContaCorrentePreConfigurada()
        {
            return new ContaCorrenteRoot
            {
                Cliente = new Cliente("Luiz", "82886830100"),
                Saldo = 0,
                ValorTransacao = 0,
                TipoMovimentacao = 0,
            };

        }
    }
}
