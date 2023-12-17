using Domain.MaterialManagement.MaterialAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Write.EntityConfigurations.MaterialManagement;

internal sealed class MaterialEntityTypeConfiguration : IEntityTypeConfiguration<Material>
{
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        builder.ToTable(nameof(Material));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code).HasColumnType("nvarchar(200)").IsRequired();
        builder.HasIndex(x => x.Code).IsUnique();
        builder.Property(x=>x.Name).HasColumnType("nvarchar(500)").IsRequired();
        // builder.Property(x => x.CodeUnique).HasColumnType("varchar(2000)").IsRequired();
        // builder.Property(x => x.CodeUnique).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        builder.Ignore(x => x.DomainEvents);

        builder.OwnsOne(x => x.Attributes, j =>
        {
            //j.Property(l => l.Name).HasColumnName(nameof(MaterialAttributes.Name)).HasColumnType("nvarchar(500)").IsRequired();
            j.Property(l => l.Varian).HasColumnName(nameof(MaterialAttributes.Varian)).HasColumnType("nvarchar(200)").IsRequired(false);
            j.Property(l => l.Unit).HasColumnName(nameof(MaterialAttributes.Unit)).HasColumnType("nvarchar(200)").IsRequired(false);
            j.Property(l => l.ColorCode).HasColumnName(nameof(MaterialAttributes.ColorCode)).HasColumnType("nvarchar(200)").IsRequired(false);
            j.Property(l => l.Weight).HasColumnName(nameof(MaterialAttributes.Weight)).HasColumnType("nvarchar(200)").IsRequired(false);
            j.Property(l => l.Width).HasColumnName(nameof(MaterialAttributes.Width)).HasColumnType("nvarchar(200)").IsRequired(false);
        });

        builder.HasOne(x => x.MaterialType)
            .WithMany()
            .HasForeignKey(ShadowProperties.MaterialTypeId)
            .IsRequired();


        builder.HasOne(x => x.RegionalMarket)
            .WithMany()
            .HasForeignKey(ShadowProperties.RegionalMarketId)
            .IsRequired();

        //builder
        //    .Property(x => x.MaterialType).HasColumnName("MaterialTypeId")
        //    .HasConversion(x => x.Id
        //        , x => MaterialType.FromId(x).Value);
        //builder
        //   .Property(x => x.RegionalMarket).HasColumnName("RegionalMarketId")
        //   .HasConversion(x => x.Id
        //       , x => RegionalMarket.FromId(x).Value);

        builder
            .HasMany(x => x.MaterialCostManagements)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade)
            .Metadata.PrincipalToDependent!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
