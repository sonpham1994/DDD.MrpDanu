using Domain.ProductManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Write.EntityConfigurations.ProductManagement;

internal sealed class BoMRevisionEntityTypeConfiguration : IEntityTypeConfiguration<BoMRevision>
{
    public void Configure(EntityTypeBuilder<BoMRevision> builder)
    {
        builder.ToTable(nameof(BoMRevision));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("smallint").UseHiLo("bomrevisionseq");

        builder.Property(x => x.Code).HasColumnType("char(14)").IsRequired();
        builder.Property(x => x.Confirmation).HasColumnType("nvarchar(500)").IsRequired();

        builder.Property(x => x.Code)
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        
        builder.HasIndex(x => x.Code).IsUnique();

        builder.HasMany(x => x.BoMRevisionMaterials)
            ?.WithOne()
            ?.Metadata?.PrincipalToDependent?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}