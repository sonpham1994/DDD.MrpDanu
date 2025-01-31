using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using Infrastructure.Persistence.SupplyAndProductionManagement.SharedKernel.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.SupplyAndProductionManagement.SupplyChainManagement.Materials.Writes.EntityConfigurations;

internal sealed class MaterialTypeEntityTypeConfiguration : BaseEnumerationConfiguration<MaterialType>;

internal sealed class RegionalMarketEntityTypeConfiguration : BaseEnumerationConfiguration<RegionalMarket>
{
    protected override void ConfigureAdditionalConfigurations(EntityTypeBuilder<RegionalMarket> builder)
    {
        builder.Property(x => x.Code).HasColumnType("varchar(50)").IsRequired();
    }
}