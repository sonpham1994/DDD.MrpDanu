using Domain.ProductionPlanning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Writes.EntityConfigurations.ProductionPlanning;

internal sealed class BoMRevisionEntityTypeConfiguration : IEntityTypeConfiguration<BoMRevision>
{
    public void Configure(EntityTypeBuilder<BoMRevision> builder)
    {
        builder.ToTable(nameof(BoMRevision));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion<BoMRevisionIdConverter>();
        builder.Property(x => x.Id).HasColumnType("smallint").UseHiLo("bomrevisionseq");

        builder.Property(x => x.Confirmation).HasColumnType("nvarchar(500)").IsRequired();

        builder.OwnsOne(x => x.Revision, j =>
        {
            j.Property(x => x.Value)
                .HasColumnType("char(14)")
                .HasColumnName(nameof(BoMRevision.Revision))
                .IsRequired();
            j.HasIndex(x => x.Value).IsUnique();
            j.Property(x => x.Value)
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });

        builder.Property(x => x.BoMId).HasConversion<BoMIdConverter>();
        
        builder.HasMany(x => x.BoMRevisionMaterials)
            ?.WithOne()
            ?.Metadata?.PrincipalToDependent?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}