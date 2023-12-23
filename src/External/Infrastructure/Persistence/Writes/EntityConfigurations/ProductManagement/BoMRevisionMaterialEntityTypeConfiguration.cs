using Domain.MaterialManagement.MaterialAggregate;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Domain.ProductManagement;
using Infrastructure.Persistence.Writes.EntityConfigurations.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Writes.EntityConfigurations.ProductManagement;

internal sealed class BoMRevisionMaterialEntityTypeConfiguration : IEntityTypeConfiguration<BoMRevisionMaterial>
{
    public void Configure(EntityTypeBuilder<BoMRevisionMaterial> builder)
    {
        builder.ToTable(nameof(BoMRevisionMaterial));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion<BoMRevisionMaterialIdConverter>();
        
        builder.OwnsOne(x => x.MaterialSupplierIdentity, j =>
        {
            // builder.HasOne(x => x.TransactionalPartner)
            //     .WithMany()
            //     .HasForeignKey("TransactionalPartnerId")
            //     .IsRequired();
            // builder.HasOne(x => x.Material)
            //     .WithMany()
            //     .HasForeignKey("MaterialId")
            //     .IsRequired();
            // we will use this to achieve separate bounded context, bounded contexts don't depend on each other
            // the Material and Transactional partner belong to a different bounded context, so we will use this
            //one would be better.
            j.Property("_transactionalPartnerId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName(nameof(BoMRevisionMaterial.MaterialSupplierIdentity.SupplierId))
                .HasConversion<TransactionalPartnerIdConverter>()
                .IsRequired();
            j.HasOne<TransactionalPartner>()
                .WithMany()
                .HasForeignKey("_transactionalPartnerId");

            j.Property(k => k.MaterialId)
                .HasColumnName(nameof(BoMRevisionMaterial.MaterialSupplierIdentity.MaterialId))
                .HasConversion<MaterialIdConverter>()
                .IsRequired();
            j.HasOne<Material>()
                .WithMany()
                .HasForeignKey(x => x.MaterialId);
        });

        builder.OwnsOne(x => x.Price, k =>
        {
            k.Property(l => l.Value)
                .HasColumnName(nameof(BoMRevisionMaterial.Price))
                .HasColumnType("decimal(18,2)").IsRequired();

            k.Property<byte>("CurrencyTypeId").HasColumnName("CurrencyTypeId");
            k.HasOne(x => x.CurrencyType)
                .WithMany()
                .HasForeignKey("CurrencyTypeId")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        });
        
        builder
            .Property(x => x.Unit)
            .HasColumnName(nameof(BoMRevisionMaterial.Unit))
            .HasColumnType("decimal(18,2)")
            .IsRequired()
            .HasConversion(x 
                    => x.Value
                , x => Unit.Create(x).Value);

        builder.HasOne<BoMRevision>()
            .WithMany(x => x.BoMRevisionMaterials)
            .HasForeignKey(nameof(BoMRevisionMaterial.BoMRevisionId))
            .IsRequired();
    }
}