using Microservices.FluxoCaixa.Core.Common;
using Microsoft.EntityFrameworkCore;
using Microservices.FluxoCaixa.Core.Entities.Movimentacao;
using Microservices.FluxoCaixa.Infrastructure.Mapping;

namespace Microservices.FluxoCaixa.Infrastructure.Persistence
{
    public class FluxoCaixaContext : DbContext
    {
        public FluxoCaixaContext(DbContextOptions<FluxoCaixaContext> options) : base(options)
        {
        }

        public DbSet<ContaCorrenteRoot> ContaCorrente { get; set; }
        public DbSet<Cliente> Cliente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Ignore<Notification>();
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new ContaCorrenteMap());
            //modelBuilder.Seed();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                entry.Entity.UltimaModificacaoPor = "Luiz";
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.DataCriacao = DateTime.Now;
                        entry.Entity.CriadoPor = "Luiz";
                        break;
                    case EntityState.Modified:
                        entry.Entity.DataUltimaAlteracao = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
