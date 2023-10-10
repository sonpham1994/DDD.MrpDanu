using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Benchmark.Infrastructure.EnumerationLoading.Setups

{
    public sealed class MaterialEntityTypeConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable(nameof(Material));
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.MaterialType)
                .WithMany()
                .HasForeignKey("MaterialTypeId")
                .IsRequired();


            builder.HasOne(x => x.RegionalMarket)
                .WithMany()
                .HasForeignKey("RegionalMarketId")
                .IsRequired();
        }
    }
}
