using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Write.EntityConfigurations.MaterialManagement;

internal sealed class ContactPersonInformationEntityTypeConfiguration : IEntityTypeConfiguration<ContactPersonInformation>
{
    public void Configure(EntityTypeBuilder<ContactPersonInformation> builder)
    {
        builder.ToTable(nameof(ContactPersonInformation));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion<ContactPersonInformationIdConverter>();

        builder.Property(x => x.Name)
            .HasColumnName(nameof(ContactPersonInformation.Name))
            .HasColumnType("nvarchar(200)").IsRequired()
            .HasConversion(
                x => x.Value,
                x => PersonName.Create(x).Value);

        builder.OwnsOne(x => x.ContactInformation, j =>
        {
            j.Property(k => k.Email)
                .HasColumnName(nameof(ContactInformation.Email))
                .HasColumnType("nvarchar(200)")
                .IsRequired(false);
            j.Property(k => k.TelNo)
                .HasColumnName(nameof(ContactInformation.TelNo))
                .HasColumnType("varchar(50)")
                .IsRequired(false);

            /*we cannot use Unique for Email and TelNo, because email or telNo can be empty by Domain rule. Hence
             * if email is empty string, it also treat that value as unique data in database, and when we persist
             * different TelNo data but empty value Email twice. It will throw an exception. If we use the combination
             * of Email and TelNo, the db can work but the business rule is incorrect, we cannot have the same
             * email with different phone numbers
             */
        });
    }
}