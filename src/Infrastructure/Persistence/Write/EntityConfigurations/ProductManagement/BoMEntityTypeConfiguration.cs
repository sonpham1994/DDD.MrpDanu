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
        builder.Property(x => x.Code)
            .HasColumnType("char(10)")
            .IsRequired();

        builder.HasIndex(x => x.Code).IsUnique();

        builder.Property(x => x.Code)
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        builder.HasMany(x => x.BoMRevisions)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade)
            .Metadata.PrincipalToDependent!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}