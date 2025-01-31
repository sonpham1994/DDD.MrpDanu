using Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
using Infrastructure.Persistence.SupplyAndProductionManagement.SharedKernel.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartners.Writes.EntityConfigurations;

internal sealed class TransactionalPartnerTypeEntityTypeConfiguration : BaseEnumerationConfiguration<TransactionalPartnerType>;

internal sealed class LocationTypeTypeEntityTypeConfiguration : BaseEnumerationConfiguration<LocationType>;

internal sealed class CountryTypeEntityTypeConfiguration : BaseEnumerationConfiguration<Country>
{
    protected override void ConfigureAdditionalConfigurations(EntityTypeBuilder<Country> builder)
    {
        builder.Property(x => x.Code).HasColumnType("varchar(50)").IsRequired();
    }
}



