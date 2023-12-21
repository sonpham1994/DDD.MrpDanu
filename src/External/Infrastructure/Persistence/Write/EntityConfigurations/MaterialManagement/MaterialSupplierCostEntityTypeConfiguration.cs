using Domain.MaterialManagement.MaterialAggregate;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Infrastructure.Persistence.Write.EntityConfigurations.MaterialManagement.StronglyTypeIdConfigurations;
using Infrastructure.Persistence.Write.EntityConfigurations.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Write.EntityConfigurations.MaterialManagement;

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

        builder.OwnsOne(x => x.MaterialCost, j =>
        {
            j.OwnsOne(k => k.Price, l =>
            {
                l.Property(k => k.Value)
                    .HasColumnType("decimal(18,2)")
                    .HasColumnName(nameof(MaterialSupplierCost.MaterialCost.Price))
                    .IsRequired();

                l.Property<byte>("CurrencyTypeId").HasColumnName("CurrencyTypeId");
                l.HasOne(k => k.CurrencyType)
                    .WithMany()
                    .HasForeignKey("CurrencyTypeId")
                    .IsRequired()
                    .OnDelete(DeleteBehavior.NoAction);
            });

            j.Property("_transactionalPartnerId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName(nameof(MaterialSupplierCost.MaterialCost.SupplierId))
                .HasConversion<TransactionalPartnerIdConverter>()
                .IsRequired();
            j.HasOne<TransactionalPartner>()
                .WithMany()
                .HasForeignKey("_transactionalPartnerId");

            //j.Property(k => k.MaterialId)
                //.HasColumnName(nameof(MaterialSupplierCost.MaterialCost.MaterialId))
                //.HasConversion<MaterialIdConverter>()
                //.IsRequired();
            //j.HasOne<Material>()
            //    .WithMany()
            //    .HasForeignKey(x => x.MaterialId);
        });

        // builder
        //     .HasOne<Material>()
        //     .WithMany(x => x.MaterialSupplierCosts)
        //     .HasForeignKey(x => x.MaterialCost.MaterialId)
        //     .IsRequired();
    }
}
