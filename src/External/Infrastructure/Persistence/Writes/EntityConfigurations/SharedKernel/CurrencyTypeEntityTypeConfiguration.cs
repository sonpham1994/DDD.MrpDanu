using Domain.SharedKernel.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Write.EntityConfigurations.SharedKernel;

internal sealed class CurrencyTypeEntityTypeConfiguration : IEntityTypeConfiguration<CurrencyType>
{
    public void Configure(EntityTypeBuilder<CurrencyType> builder)
    {
        builder.ToTable(nameof(CurrencyType));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever().HasDefaultValue(1);

        builder.Property(x => x.Name).HasColumnType("varchar(50)").IsRequired();
    }
}
