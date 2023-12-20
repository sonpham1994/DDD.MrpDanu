using Domain.MaterialManagement.MaterialAggregate;
using Infrastructure.Persistence.Write.EntityConfigurations.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Write.EntityConfigurations.MaterialManagement;

internal sealed class MaterialEntityTypeConfiguration : IEntityTypeConfiguration<Material>
{
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        builder.ToTable(nameof(Material));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion<MaterialIdConverter>()
            /*
             * {MaterialId { Value = 0eb5ae1e-efe2-484d-ad3b-3b4c1db4a69c }}
                {MaterialId { Value = 9d309f77-7e81-4c1a-beba-a1eb241c397f }}
                {MaterialId { Value = 6f994790-6207-4757-9faa-e81a1aa7796f }}
                {MaterialId { Value = ffe6a145-001f-466c-849f-4c48cd9cb432 }}
                with this, strongly typed id generate Guid.NewGuid, not Sequential Guid, need to research to how it generate Sequential Guid from
                SqlGuid
             */
            .ValueGeneratedOnAdd();
        builder.Property(x => x.Code).HasColumnType("nvarchar(200)").IsRequired();
        builder.HasIndex(x => x.Code).IsUnique();
        builder.Property(x => x.Name).HasColumnType("nvarchar(500)").IsRequired();
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
            .HasMany(x => x.MaterialSupplierCosts)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade)
            .Metadata.PrincipalToDependent!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
