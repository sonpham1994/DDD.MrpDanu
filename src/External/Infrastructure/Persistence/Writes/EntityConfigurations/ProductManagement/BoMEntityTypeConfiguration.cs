using Domain.ProductManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Write.EntityConfigurations.ProductManagement;

internal sealed class BoMEntityTypeConfiguration : IEntityTypeConfiguration<BoM>
{
    public void Configure(EntityTypeBuilder<BoM> builder)
    {
        builder.ToTable(nameof(BoM));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion<BoMIdConverter>();
        builder.Ignore(x => x.DomainEvents);

        builder.OwnsOne(x => x.Revision, j =>
        {
            j.Property(x => x.Value)
                .HasColumnType("char(10)")
                .HasColumnName(nameof(BoM.Revision))
                .IsRequired();
            j.HasIndex(x => x.Value).IsUnique();
            j.Property(x => x.Value)
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });
        
        
        builder.Property(x => x.ProductId).HasConversion<ProductIdConverter>();
        builder.HasOne<Product>()
            .WithOne()
            .HasForeignKey<Product>(x => x.BoMId)
            .IsRequired(false);

        builder.HasMany(x => x.BoMRevisions)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade)
            .Metadata.PrincipalToDependent!.SetPropertyAccessMode(PropertyAccessMode.Field);
        
    }
}