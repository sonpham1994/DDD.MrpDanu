using Domain.ProductManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Write.EntityConfigurations.ProductManagement;

internal sealed class BoMRevisionMaterialEntityTypeConfiguration : IEntityTypeConfiguration<BoMRevisionMaterial>
{
    public void Configure(EntityTypeBuilder<BoMRevisionMaterial> builder)
    {
        builder.ToTable(nameof(BoMRevisionMaterial));
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.Price, j =>
        {
            j.Property(k => k.Value)
                .HasColumnName(nameof(BoMRevisionMaterial.Price))
                .HasColumnType("decimal(18,2)").IsRequired();

            j.Property<byte>("CurrencyTypeId").HasColumnName("CurrencyTypeId");
            j.HasOne(x => x.CurrencyType)
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
            .HasConversion(x => x.Value, x => Unit.Create(x).Value);

        builder.HasOne(x => x.TransactionalPartner)
            .WithMany()
            .HasForeignKey("TransactionalPartnerId")
            .IsRequired();

        builder.HasOne<BoMRevision>()
            .WithMany(x => x.BoMRevisionMaterials)
            .HasForeignKey("BoMRevisionId")
            .IsRequired();

        builder.HasOne(x => x.Material)
            .WithMany()
            .HasForeignKey("MaterialId")
            .IsRequired();
    }
}