using Microservices.FluxoCaixa.Application.Dtos;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Microservices.FluxoCaixa.Infrastructure.Persistence
{
    public class FluxoCaixaMongoContext : IFluxoCaixaMongoContext
    {
        public FluxoCaixaMongoContext(IConfiguration configuration)
        {            

            var client = new MongoClient(GetConnectionString(configuration));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Extratos = database.GetCollection<ExtratoDto>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));

        }
        private string GetConnectionString(IConfiguration configuration)
        {
            var server = configuration.GetValue<string>("DatabaseSettings:DbServer") ?? "localhost";
            var port = configuration.GetValue<string>("DatabaseSettings:DbPort") ?? "27017";
            var mongoConnection = $"mongodb://{server}:{port}";
            return mongoConnection;
        }
        public IMongoCollection<ExtratoDto> Extratos { get; }
    }
}
