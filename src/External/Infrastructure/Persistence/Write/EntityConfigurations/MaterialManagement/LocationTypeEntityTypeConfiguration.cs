using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Write.EntityConfigurations.MaterialManagement;

internal sealed class LocationTypeEntityTypeConfiguration : IEntityTypeConfiguration<LocationType>
{
    public void Configure(EntityTypeBuilder<LocationType> builder)
    {
        builder.ToTable(nameof(LocationType));
        builder.HasKey(e => e.Id);

        builder.Property(x => x.Id).ValueGeneratedNever().HasDefaultValue(1);

        builder.Property(x => x.Name).HasColumnType("varchar(50)").IsRequired();
    }
}
