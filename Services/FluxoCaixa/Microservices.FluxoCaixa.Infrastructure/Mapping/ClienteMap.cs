using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microservices.FluxoCaixa.Core.Entities.Movimentacao;

namespace Microservices.FluxoCaixa.Infrastructure.Mapping
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Nome).HasColumnName("Nome");

            builder.OwnsOne(x => x.CPF)
               .Property(x => x.Numero)
               .HasColumnName("CPF").IsRequired();


        }
    }
}
