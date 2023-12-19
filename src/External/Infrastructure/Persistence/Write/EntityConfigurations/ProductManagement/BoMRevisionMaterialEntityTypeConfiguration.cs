using Domain.MaterialManagement.MaterialAggregate;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Domain.ProductManagement;
using Infrastructure.Persistence.Write.EntityConfigurations.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Write.EntityConfigurations.ProductManagement;

internal sealed class BoMRevisionMaterialEntityTypeConfiguration : IEntityTypeConfiguration<BoMRevisionMaterial>
{
    public void Configure(EntityTypeBuilder<BoMRevisionMaterial> builder)
    {
        builder.ToTable(nameof(BoMRevisionMaterial));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion<BoMRevisionMaterialIdConverter>();
        
        builder.OwnsOne(x => x.MaterialCost, j =>
        {
            j.OwnsOne(x => x.Price, k =>
            {
                k.Property(l => l.Value)
                    .HasColumnName(nameof(BoMRevisionMaterial.MaterialCost.Price))
                    .HasColumnType("decimal(18,2)").IsRequired();

                k.Property<byte>("CurrencyTypeId").HasColumnName("CurrencyTypeId");
                k.HasOne(x => x.CurrencyType)
                    .WithMany()
                    .HasForeignKey("CurrencyTypeId")
                    .IsRequired()
                    .OnDelete(DeleteBehavior.NoAction);
            });
            
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
            j.Property(k => k.SupplierId).HasConversion<SupplierIdConverter>();
            j.HasOne<TransactionalPartner>()
                .WithMany()
                .HasForeignKey(x => x.SupplierId)
                .IsRequired();

            j.Property(k => k.MaterialId).HasConversion<MaterialIdConverter>();
            j.HasOne<Material>()
                .WithMany()
                .HasForeignKey(x => x.MaterialId)
                .IsRequired();
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