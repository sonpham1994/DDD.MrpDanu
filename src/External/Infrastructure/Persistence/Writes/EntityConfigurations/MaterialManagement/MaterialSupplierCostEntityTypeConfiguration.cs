using Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel.ValueObjects;
using Infrastructure.Persistence.Writes.EntityConfigurations.SupplyChainManagement.StronglyTypeIdConfigurations;
using Infrastructure.Persistence.Writes.EntityConfigurations.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

namespace Infrastructure.Persistence.Writes.EntityConfigurations.SupplyChainManagement;

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

        // for MaterialId, due to Material will automatically define the relationships between Material and MaterialSupplierCost
        // it will auto generate MaterialId even though we don't specify configuration in Material. But Material will map
        // MaterialSupplierCost in MaterialSupplierCost entity, and we specify MaterialId in owned entity type. So it will
        //generates an additional column like `MaterialSupplierCost_MaterialId`. By configuring this, we solve that problem
        // by using shadow property.
        builder.Property<MaterialId>(nameof(MaterialSupplierCost.MaterialSupplierIdentity.MaterialId))
            .HasColumnName(nameof(MaterialSupplierCost.MaterialSupplierIdentity.MaterialId))
            .HasConversion<MaterialIdConverter>();
        builder.OwnsOne(x => x.MaterialSupplierIdentity, j =>
        {
            j.Property<TransactionalPartnerId>("_transactionalPartnerId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName(nameof(MaterialSupplierCost.MaterialSupplierIdentity.SupplierId))
                .HasConversion<TransactionalPartnerIdConverter>()
                .IsRequired();
            j.HasOne<TransactionalPartner>()
                .WithMany()
                .HasForeignKey("_transactionalPartnerId");

            // for MaterialId, due to Material will automatically define the relationships between Material and MaterialSupplierCost
            // it will auto generate MaterialId even though we don't specify configuration in Material. But Material will map
            // MaterialSupplierCost in MaterialSupplierCost entity, and we specify MaterialId in owned entity type. So it will
            //generates an additional column like `MaterialSupplierCost_MaterialId`. By configuring this, we solve that problem
            // by using shadow property.
            j.Property(k => k.MaterialId)
                .HasColumnName(nameof(MaterialSupplierCost.MaterialSupplierIdentity.MaterialId))
                .HasConversion<MaterialIdConverter>();
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


    // .Net 8
    //public void Configure(EntityTypeBuilder<MaterialSupplierCost> builder)
    //{
    //    builder.ToTable(nameof(MaterialSupplierCost));
    //    builder.HasKey(x => x.Id);
    //    builder.Property(x => x.Id)
    //        .HasConversion<MaterialSupplierCostIdConverter>()
    //        .HasValueGenerator<MaterialSupplierCostIdValueGenerator>();

    //    builder.Property(k => k.MinQuantity)
    //           .HasColumnType("int")
    //           .HasColumnName(nameof(MaterialSupplierCost.MinQuantity))
    //           .IsRequired();
    //    builder.Property<byte>("CurrencyTypeId").HasColumnName("CurrencyTypeId");
    //    builder.HasOne<CurrencyType>()
    //            .WithMany()
    //            .HasForeignKey("CurrencyTypeId")
    //            .IsRequired()
    //            .OnDelete(DeleteBehavior.NoAction);
    //    builder.ComplexProperty(x => x.Surcharge, j =>
    //    {
    //        j.Property(k => k.Value)
    //            .HasColumnType("decimal(18,2)")
    //            .HasColumnName(nameof(MaterialSupplierCost.Surcharge))
    //            .IsRequired();

    //        j.Property<byte>("CurrencyTypeId").HasColumnName("CurrencyTypeId");
    //    });
    //    builder.ComplexProperty(k => k.Price, j =>
    //    {
    //        j.Property(k => k.Value)
    //            .HasColumnType("decimal(18,2)")
    //            .HasColumnName(nameof(MaterialSupplierCost.Price))
    //            .IsRequired();

    //        j.Property<byte>("CurrencyTypeId").HasColumnName("CurrencyTypeId");
    //    });


    //    // for MaterialId, due to Material will automatically define the relationships between Material and MaterialSupplierCost
    //    // it will auto generate MaterialId even though we don't specify configuration in Material. But Material will map
    //    // MaterialSupplierCost in MaterialSupplierCost entity, and we specify MaterialId in owned entity type. So it will
    //    //generates an additional column like `MaterialSupplierCost_MaterialId`. By configuring this, we solve that problem
    //    // by using shadow property.
    //    builder.Property<MaterialId>(nameof(MaterialSupplierCost.MaterialSupplierIdentity.MaterialId))
    //        .HasColumnName(nameof(MaterialSupplierCost.MaterialSupplierIdentity.MaterialId))
    //        .HasConversion<MaterialIdConverter>();

    //    builder.Property<TransactionalPartnerId>("_transactionalPartnerId")
    //            .HasColumnName(nameof(MaterialSupplierCost.MaterialSupplierIdentity.SupplierId))
    //            .HasConversion<TransactionalPartnerIdConverter>()
    //            .IsRequired();
    //    builder.HasOne<TransactionalPartner>()
    //            .WithMany()
    //            .HasForeignKey(nameof(MaterialSupplierCost.MaterialSupplierIdentity.SupplierId));

    //    builder.ComplexProperty(x => x.MaterialSupplierIdentity, j =>
    //    {
    //        j.Property<TransactionalPartnerId>("_transactionalPartnerId")
    //            .UsePropertyAccessMode(PropertyAccessMode.Field)
    //            //.HasColumnName(nameof(MaterialSupplierCost.MaterialSupplierIdentity.SupplierId))
    //            .HasConversion<TransactionalPartnerIdConverter>()
    //            .IsRequired();

    //        // for MaterialId, due to Material will automatically define the relationships between Material and MaterialSupplierCost
    //        // it will auto generate MaterialId even though we don't specify configuration in Material. But Material will map
    //        // MaterialSupplierCost in MaterialSupplierCost entity, and we specify MaterialId in owned entity type. So it will
    //        //generates an additional column like `MaterialSupplierCost_MaterialId`. By configuring this, we solve that problem
    //        // by using shadow property.
    //        j.Property(k => k.MaterialId)
    //            .HasColumnName(nameof(MaterialSupplierCost.MaterialSupplierIdentity.MaterialId))
    //            .HasConversion<MaterialIdConverter>();
    //    });

    //    // builder
    //    //     .HasOne<Material>()
    //    //     .WithMany(x => x.MaterialSupplierCosts)
    //    //     .HasForeignKey(x => x.MaterialCost.MaterialId)
    //    //     .IsRequired();
    //}
}
