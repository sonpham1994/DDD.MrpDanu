using Domain.MaterialManagement.MaterialAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Writes.EntityConfigurations.MaterialManagement;

internal sealed class MaterialTypeEntityTypeConfiguration : IEntityTypeConfiguration<MaterialType>
{
    public void Configure(EntityTypeBuilder<MaterialType> builder)
    {
        builder.ToTable(nameof(MaterialType));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnType("tinyint")
            .HasDefaultValue(1)
            .ValueGeneratedNever();

        builder.Property(x => x.Name).HasColumnType("varchar(50)").IsRequired();
    }
}
