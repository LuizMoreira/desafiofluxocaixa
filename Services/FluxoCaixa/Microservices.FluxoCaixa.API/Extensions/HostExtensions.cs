
using Microsoft.EntityFrameworkCore;

namespace Microservices.FluxoCaixa.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation("Migrando banco de dados do contexto {DbContextName}", typeof(TContext).Name);
                             
                    context.Database.Migrate();
                    seeder(context, services);

                    logger.LogInformation("Banco de dados migrado com o contexto {DbContextName}", typeof(TContext).Name);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Um erro ocorreu na migração do banco de dados do usedo no contexto {DbContextName}", typeof(TContext).Name);                   
                }
            }

            return host;
        }
    }
}
