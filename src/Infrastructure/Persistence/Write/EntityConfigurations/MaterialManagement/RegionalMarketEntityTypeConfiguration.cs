using Domain.MaterialManagement.MaterialAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Write.EntityConfigurations.MaterialManagement
{
    internal sealed class RegionalMarketEntityTypeConfiguration : IEntityTypeConfiguration<RegionalMarket>
    {
        public void Configure(EntityTypeBuilder<RegionalMarket> builder)
        {
            builder.ToTable(nameof(RegionalMarket));
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("tinyint")
                .HasDefaultValue(1)
                .ValueGeneratedNever();

            builder.Property(x => x.Code).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.Name).HasColumnType("varchar(50)").IsRequired();
        }
    }
}
