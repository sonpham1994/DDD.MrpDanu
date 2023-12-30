using Domain.SupplyChainManagement.TransactionalPartnerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Writes.EntityConfigurations.SupplyChainManagement;

internal sealed class TransactionalPartnerTypeEntityTypeConfiguration : IEntityTypeConfiguration<TransactionalPartnerType>
{
    public void Configure(EntityTypeBuilder<TransactionalPartnerType> builder)
    {
        builder.ToTable(nameof(TransactionalPartnerType));
        builder.HasKey(t => t.Id);
        builder.Property(x => x.Id).ValueGeneratedNever().HasDefaultValue(1);

        builder.Property(x => x.Name).HasColumnType("varchar(50)").IsRequired();
    }
}
