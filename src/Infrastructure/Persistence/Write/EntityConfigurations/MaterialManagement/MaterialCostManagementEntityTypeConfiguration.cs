using Domain.MaterialManagement.MaterialAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Write.EntityConfigurations.MaterialManagement
{
    internal sealed class MaterialCostManagementEntityTypeConfiguration : IEntityTypeConfiguration<MaterialCostManagement>
    {
        public void Configure(EntityTypeBuilder<MaterialCostManagement> builder)
        {
            builder.ToTable(nameof(MaterialCostManagement));
            builder.HasKey(x => x.Id);
            builder.Property(k => k.MinQuantity)
                   .HasColumnType("int")
                   .HasColumnName(nameof(MaterialCostManagement.MinQuantity))
                   .IsRequired();

            builder.OwnsOne(x => x.Surcharge, j =>
            {
                j.Property(k => k.Value)
                    .HasColumnType("decimal(18,2)")
                    .HasColumnName(nameof(MaterialCostManagement.Surcharge))
                    .IsRequired();

                j.Property<byte>("CurrencyTypeId").HasColumnName("CurrencyTypeId");
                j.HasOne(k => k.CurrencyType)
                    .WithMany()
                    .HasForeignKey("CurrencyTypeId")
                    .IsRequired()
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.OwnsOne(x => x.Price, j =>
            {
                j.Property(k => k.Value)
                    .HasColumnType("decimal(18,2)")
                    .HasColumnName(nameof(MaterialCostManagement.Price))
                    .IsRequired();

                j.Property<byte>("CurrencyTypeId").HasColumnName("CurrencyTypeId");
                j.HasOne(k => k.CurrencyType)
                    .WithMany()
                    .HasForeignKey("CurrencyTypeId")
                    .IsRequired()
                    .OnDelete(DeleteBehavior.NoAction);

            });

            builder
                .HasOne<Material>()
                .WithMany(x => x.MaterialCostManagements)
                .HasForeignKey("MaterialId")
                .IsRequired();
        }
    }
}
