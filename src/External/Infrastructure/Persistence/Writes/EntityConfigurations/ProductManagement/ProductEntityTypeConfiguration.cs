using Domain.SupplyAndProductionManagement.ProductionPlanning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Writes.EntityConfigurations.ProductionPlanning;

internal sealed class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(nameof(Product));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion<ProductIdConverter>();

        //https://stackoverflow.com/questions/282099/whats-the-hi-lo-algorithm?fbclid=IwAR0gh1qjDZaeV0s0-rCztmYW0KGyisHuYTGz_NyqlGUW7lq8V3wlBKtOlCs
        //https://davecallan.com/how-to-use-hilo-with-entity-framework/?fbclid=IwAR2cMTVzef6ArfUv804WyWyHvIfJpkFtoJ38nDdHJgOM7Dl-dXTNHhVxz2Y
        //https://davecallan.com/entity-framework-7-performance-improvements/?fbclid=IwAR2_0F7NalNCpJdP4t59yAv9PbhneM8GDJu_J9fsYqFM6j0Y_Ga9l9L_McI
        // use HiLo for non-key property: https://github.com/dotnet/efcore/issues/29758
        builder.Property(x => x.Id).UseHiLo("productseq");
        builder.Property(x => x.Code).HasColumnType("nvarchar(50)").IsRequired();
        builder.Property(x => x.Name).HasColumnType("nvarchar(50)").IsRequired();
        builder.Property(x => x.Description).HasColumnType("nvarchar(200)").IsRequired();

        builder.Ignore(x => x.DomainEvents);

        //One-To-One relationship (case: PK-to-PK relationship https://learn.microsoft.com/en-gb/ef/core/modeling/relationships/one-to-one)
        // builder.HasOne(x => x.BoM)
        //     .WithOne()
        //     .HasForeignKey<BoM>()
        //     .IsRequired(false);

        builder.Property(x => x.BoMId)
            .HasConversion<BoMIdConverter>();
        builder.HasOne<BoM>()
            .WithOne()
            .HasForeignKey<BoM>(x => x.ProductId)
            .IsRequired(false);
    }
}