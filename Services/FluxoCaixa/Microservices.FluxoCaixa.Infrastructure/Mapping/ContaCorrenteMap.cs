using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microservices.FluxoCaixa.Core.Entities.Movimentacao;

namespace Microservices.FluxoCaixa.Infrastructure.Mapping
{
    public class ContaCorrenteMap : IEntityTypeConfiguration<ContaCorrenteRoot>
    {
        public void Configure(EntityTypeBuilder<ContaCorrenteRoot> builder)
        {
            builder.HasKey(x => x.Id).HasName("PK_IDCONTACORRENTE");
            builder.Property(c => c.Saldo).HasColumnName("Saldo").HasPrecision(14, 2);
            builder.Property(c => c.ValorTransacao).HasColumnName("ValorTransacao").HasPrecision(14, 2);
            builder.Property(x => x.TipoMovimentacao);
            builder.Property(p => p.RowVersion).IsConcurrencyToken();

        }
    }
}
