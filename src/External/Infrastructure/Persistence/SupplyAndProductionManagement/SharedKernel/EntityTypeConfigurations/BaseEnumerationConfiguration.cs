using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel;

namespace Infrastructure.Persistence.SupplyAndProductionManagement.SharedKernel.EntityTypeConfigurations;

internal abstract class BaseEnumerationConfiguration<T> : IEntityTypeConfiguration<T> where T : Enumeration<T>
{
    protected virtual string TableName => typeof(T).Name;
    
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.ToTable(TableName);
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id).ValueGeneratedNever().HasDefaultValue(1);
        builder.Property(x => x.Name).HasColumnType("varchar(50)").IsRequired();
        
        ConfigureAdditionalConfigurations(builder);
    }

    protected virtual void ConfigureAdditionalConfigurations(EntityTypeBuilder<T> builder)
    {
        return;
    }
}