using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microservices.FluxoCaixa.Infrastructure.Persistence;
using Microservices.FluxoCaixa.Application.Contracts.Persistence;
using Microservices.FluxoCaixa.Infrastructure.Repositories;
using Microservico.Separacao.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Microservices.FluxoCaixa.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 2)); // Example version 8.0.30

            services.AddDbContext<FluxoCaixaContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(GetConnectionString(configuration), ServerVersion.AutoDetect(GetConnectionString(configuration)), options => options.EnableRetryOnFailure(
                         maxRetryCount: 5,
                         maxRetryDelay: System.TimeSpan.FromSeconds(30),    
                         errorNumbersToAdd: null))
                    // remover para produção.
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );
            // services.AddDbContext<FluxoCaixaContext>(
            //     options => options.UseNpgsql(GetPostgreSqlConnectionString(configuration))
            // );
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();
            services.AddScoped<IExtratoRepository, ExtratoRepository>();

            //mongodb
            services.AddScoped<IFluxoCaixaMongoContext, FluxoCaixaMongoContext>();
            services.AddScoped<IExtratoRepository, ExtratoRepository>();
            return services;
        }
        private static string GetConnectionString(IConfiguration configuration)
        {

            var server = configuration.GetValue<string>("ConnectionStrings:DbServer") ?? "localhost";
            var port = configuration.GetValue<string>("ConnectionStrings:DbPort") ?? "3306";
            var user = configuration.GetValue<string>("ConnectionStrings:DbUser") ?? "inmetrics";
            var password = configuration.GetValue<string>("ConnectionStrings:DbPassword") ?? "pws1234";
            var database = configuration.GetValue<string>("ConnectionStrings:DbName") ?? "dbmysql";
            return $"server={server};port={port};user={user};password={password};database={database}";
        }
                private static string GetPostgreSqlConnectionString(IConfiguration configuration)
        {
            var server = configuration.GetValue<string>("ConnectionStrings:PostgreSqlServer") ?? "localhost";
            var port = configuration.GetValue<string>("ConnectionStrings:PostgreSqlPort") ?? "5432";
            var user = configuration.GetValue<string>("ConnectionStrings:PostgreSqlUser") ?? "postgres";
            var password = configuration.GetValue<string>("ConnectionStrings:PostgreSqlPassword") ?? "pws1234";
            var database = configuration.GetValue<string>("ConnectionStrings:PostgreSqlDatabase") ?? "dbpostgres";
            return $"Host={server};Port={port};Username={user};Password={password};Database={database}";
        }
    }
}
