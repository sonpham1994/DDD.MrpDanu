using Domain.MaterialManagement.MaterialAggregate;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Infrastructure.Persistence.Writes.EntityConfigurations.MaterialManagement.StronglyTypeIdConfigurations;
using Infrastructure.Persistence.Writes.EntityConfigurations.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Writes.EntityConfigurations.MaterialManagement;

internal sealed class MaterialSupplierCostEntityTypeConfiguration : IEntityTypeConfiguration<MaterialSupplierCost>
{
    public void Configure(EntityTypeBuilder<MaterialSupplierCost> builder)
    {
        builder.ToTable(nameof(MaterialSupplierCost));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion<MaterialSupplierCostIdConverter>()
            .HasValueGenerator<MaterialSupplierCostIdValueGenerator>();
        
        builder.Property(k => k.MinQuantity)
               .HasColumnType("int")
               .HasColumnName(nameof(MaterialSupplierCost.MinQuantity))
               .IsRequired();

        builder.OwnsOne(x => x.Surcharge, j =>
        {
            j.Property(k => k.Value)
                .HasColumnType("decimal(18,2)")
                .HasColumnName(nameof(MaterialSupplierCost.Surcharge))
                .IsRequired();

            j.Property<byte>("CurrencyTypeId").HasColumnName("CurrencyTypeId");
            j.HasOne(k => k.CurrencyType)
                .WithMany()
                .HasForeignKey("CurrencyTypeId")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        });

        builder.OwnsOne(x => x.MaterialSupplierIdentity, j =>
        {
            j.Property("_transactionalPartnerId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName(nameof(MaterialSupplierCost.MaterialSupplierIdentity.SupplierId))
                .HasConversion<TransactionalPartnerIdConverter>()
                .IsRequired();
            j.HasOne<TransactionalPartner>()
                .WithMany()
                .HasForeignKey("_transactionalPartnerId");

            // in Material, we define MaterialSupplierCost, so we don't need to specify here
            //j.Property(k => k.MaterialId)
                //.HasColumnName(nameof(MaterialSupplierCost.MaterialCost.MaterialId))
                //.HasConversion<MaterialIdConverter>()
                //.IsRequired();
            //j.HasOne<Material>()
            //    .WithMany()
            //    .HasForeignKey(x => x.MaterialId);
        });

        builder.OwnsOne(k => k.Price, j =>
        {
            j.Property(k => k.Value)
                .HasColumnType("decimal(18,2)")
                .HasColumnName(nameof(MaterialSupplierCost.Price))
                .IsRequired();

            j.Property<byte>("CurrencyTypeId").HasColumnName("CurrencyTypeId");
            j.HasOne(k => k.CurrencyType)
                .WithMany()
                .HasForeignKey("CurrencyTypeId")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        });
        
        // builder
        //     .HasOne<Material>()
        //     .WithMany(x => x.MaterialSupplierCosts)
        //     .HasForeignKey(x => x.MaterialCost.MaterialId)
        //     .IsRequired();
    }
}
