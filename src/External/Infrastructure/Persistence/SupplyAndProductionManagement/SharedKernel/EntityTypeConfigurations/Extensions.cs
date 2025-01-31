using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.ValueObjects;

namespace Infrastructure.Persistence.SupplyAndProductionManagement.SharedKernel.EntityTypeConfigurations;

internal static class Extensions
{
    public static void ConfigureMoney<T>(this OwnedNavigationBuilder<T, Money> moneyConfig, string columnName) where T : class
    {
        moneyConfig.Property(k => k.Value)
            .HasColumnType("money")
            .HasColumnName(columnName)
            .IsRequired();

        moneyConfig.Property<byte>(ShadowProperties.CurrencyTypeId).HasColumnName(ShadowProperties.CurrencyTypeId);
        moneyConfig.HasOne(k => k.CurrencyType)
            .WithMany()
            .HasForeignKey(ShadowProperties.CurrencyTypeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}