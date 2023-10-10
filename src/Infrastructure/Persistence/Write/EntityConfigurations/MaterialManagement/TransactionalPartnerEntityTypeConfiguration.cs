using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Write.EntityConfigurations.MaterialManagement;

internal sealed class TransactionalPartnerEntityTypeConfiguration : IEntityTypeConfiguration<TransactionalPartner>
{
    public void Configure(EntityTypeBuilder<TransactionalPartner> builder)
    {
        builder.ToTable(nameof(TransactionalPartner));
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasColumnName(nameof(TransactionalPartner.Name))
            .HasColumnType("nvarchar(300)")
            .HasConversion(x => x.Value,
                x => CompanyName.Create(x).Value);

        builder.OwnsOne(x => x.TaxNo, j =>
        {
            j.Property(k => k.Value)
                .HasColumnType("nvarchar(200)")
                .HasColumnName(nameof(TransactionalPartner.TaxNo));

            j.Property<byte>(ShadowProperties.CountryId).HasColumnName(ShadowProperties.CountryId);
            j.HasOne(x => x.Country)
                .WithMany()
                .HasForeignKey(ShadowProperties.CountryId)
                .IsRequired();
        });

        builder.Property(k => k.Website)
            .HasColumnType("varchar(50)")
            .HasColumnName(nameof(TransactionalPartner.Website))
            .HasConversion(x => x.Value, x => Website.Create(x).Value)
            .IsRequired(false);
        
        builder.HasOne(x => x.ContactPersonInformation)
            .WithOne()
            .HasForeignKey<ContactPersonInformation>()
            .IsRequired();
        
        builder.OwnsOne(x => x.Address, y =>
        {
            y.Property(j => j.City)
               .HasColumnType("nvarchar(50)")
               .HasColumnName($"{nameof(Address)}_{nameof(Address.City)}")
               .IsRequired();

            y.Property(j => j.District)
                .HasColumnType("nvarchar(50)")
                .HasColumnName($"{nameof(Address)}_{nameof(Address.District)}")
                .IsRequired();

            y.Property(j => j.Street)
                .HasColumnType("nvarchar(100)")
                .HasColumnName($"{nameof(Address)}_{nameof(Address.Street)}")
                .IsRequired();

            y.Property(j => j.Ward)
                .HasColumnType("nvarchar(50)")
                .HasColumnName($"{nameof(Address)}_{nameof(Address.Ward)}")
                .IsRequired();

            y.Property(j => j.ZipCode)
                .HasColumnType("nvarchar(50)")
                .HasColumnName($"{nameof(Address)}_{nameof(Address.ZipCode)}")
                .IsRequired();

            y.Property<byte>(ShadowProperties.CountryId).HasColumnName(ShadowProperties.CountryId);
            y.HasOne(x => x.Country)
                .WithMany()
                .HasForeignKey(ShadowProperties.CountryId)
                .IsRequired();
        });

        builder
            .HasOne(x => x.TransactionalPartnerType)
            .WithMany()
            .HasForeignKey(ShadowProperties.TransactionalPartnerTypeId).IsRequired();

        builder
            .HasOne(x => x.CurrencyType)
            .WithMany()
            .HasForeignKey(ShadowProperties.CurrencyTypeId).IsRequired();
        
        builder
            .HasOne(x => x.LocationType)
            .WithMany()
            .HasForeignKey(ShadowProperties.LocationTypeId)
            .IsRequired();
    }
}