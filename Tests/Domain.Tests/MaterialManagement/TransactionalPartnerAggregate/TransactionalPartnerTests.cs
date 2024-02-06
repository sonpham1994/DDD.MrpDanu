using Domain.SharedKernel.Enumerations;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
using FluentAssertions;
using DomainErrors = Domain.SupplyAndProductionManagement.SupplyChainManagement.DomainErrors;

namespace Domain.Tests.MaterialManagement.TransactionalPartnerAggregate;
using static MaterialManagementPreparingData;

public class TransactionalPartnerTests
{
    [Fact]
    public void Cannot_create_transactional_partner_if_location_is_oversea_and_country_is_vietnam()
    {
        var result = TransactionalPartner.Create
        (
            CompanyName1,
            TaxNo1,
            Website,
            PersonName1,
            ContactInformation1,
            Address1,
            TransactionalPartnerType.Both,
            CurrencyType.USD,
            LocationType.Oversea
        );

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.TransactionalPartner.InvalidCountryAndLocationType);
    }

    [Fact]
    public void Cannot_create_transactional_partner_if_location_is_domestic_and_country_is_not_vietnam()
    {
        var result = TransactionalPartner.Create
        (
            CompanyName1,
            TaxNo1,
            Website,
            PersonName1,
            ContactInformation1,
            OverseaAddress,
            TransactionalPartnerType.Both,
            CurrencyType.USD,
            LocationType.Domestic
        );

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.TransactionalPartner.InvalidCountryAndLocationType);
    }

    [Fact]
    public void Cannot_create_transactional_partner_if_country_is_vietnam_and_currency_is_not_vnd()
    {
        var result = TransactionalPartner.Create
        (
            CompanyName1,
            TaxNo1,
            Website,
            PersonName1,
            ContactInformation1,
            Address1,
            TransactionalPartnerType.Both,
            CurrencyType.USD,
            LocationType.Domestic
        );

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.TransactionalPartner.InvalidCurrencyType);
    }

    [Fact]
    public void Create_transactional_partner_successfully()
    {
        var result = TransactionalPartner.Create
        (
            CompanyName1,
            TaxNo1,
            Website,
            PersonName1,
            ContactInformation1,
            Address1,
            TransactionalPartnerType.Both,
            CurrencyType.VND,
            LocationType.Domestic
        );

        result.IsSuccess.Should().BeTrue();
        result.Value.Name.Should().Be(CompanyName1);
        result.Value.TaxNo.Should().Be(TaxNo1);
        result.Value.Website.Should().Be(Website!);
        result.Value.ContactPersonInformation.Name.Should().Be(PersonName1);
        result.Value.ContactPersonInformation.ContactInformation.Should().Be(ContactInformation1);
        result.Value.Address.Should().Be(Address1);
        result.Value.TransactionalPartnerType.Should().Be(TransactionalPartnerType.Both);
        result.Value.CurrencyType.Should().Be(CurrencyType.VND);
        result.Value.LocationType.Should().Be(LocationType.Domestic);
    }

    [Fact]
    public void Cannot_update_transactional_partner_if_location_is_oversea_and_country_is_vietnam()
    {
        var transactionalPartner = TransactionalPartner.Create
        (
            CompanyName1,
            TaxNo1,
            Website,
            PersonName1,
            ContactInformation1,
            Address1,
            TransactionalPartnerType.Both,
            CurrencyType.VND,
            LocationType.Domestic
        );

        var result = transactionalPartner.Value.Update
        (
            CompanyName1,
            TaxNo1,
            Website,
            Address1,
            TransactionalPartnerType.Both,
            CurrencyType.USD,
            LocationType.Oversea
        );

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.TransactionalPartner.InvalidCountryAndLocationType);
    }

    [Fact]
    public void Cannot_update_transactional_partner_if_location_is_domestic_and_country_is_not_vietnam()
    {
        var transactionalPartner = TransactionalPartner.Create
        (
            CompanyName1,
            TaxNo1,
            Website,
            PersonName1,
            ContactInformation1,
            Address1,
            TransactionalPartnerType.Both,
            CurrencyType.VND,
            LocationType.Domestic
        );

        var result = transactionalPartner.Value.Update
        (
            CompanyName1,
            TaxNo1,
            Website,
            OverseaAddress,
            TransactionalPartnerType.Both,
            CurrencyType.USD,
            LocationType.Domestic
        );

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.TransactionalPartner.InvalidCountryAndLocationType);
    }

    [Fact]
    public void Cannot_update_transactional_partner_if_country_is_vietnam_and_currency_is_not_vnd()
    {
        var transactionalPartner = TransactionalPartner.Create
        (
            CompanyName1,
            TaxNo1,
            Website,
            PersonName1,
            ContactInformation1,
            Address1,
            TransactionalPartnerType.Both,
            CurrencyType.VND,
            LocationType.Domestic
        );

        var result = transactionalPartner.Value.Update
        (
            CompanyName1,
            TaxNo1,
            Website,
            Address1,
            TransactionalPartnerType.Both,
            CurrencyType.USD,
            LocationType.Domestic
        );

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.TransactionalPartner.InvalidCurrencyType);
    }

    [Fact]
    public void Update_transactional_partner_successfully()
    {
        var transactionalPartner = TransactionalPartner.Create
        (
            CompanyName1,
            TaxNo1,
            Website.Create(string.Empty).Value,
            PersonName1,
            ContactInformation1,
            Address1,
            TransactionalPartnerType.Both,
            CurrencyType.VND,
            LocationType.Domestic
        );

        var personName = PersonName.Create("Name2").Value;
        var contactInfo = ContactInformation.Create(string.Empty, "abcxyz@gmail.com").Value;
        var result = transactionalPartner.Value.Update
        (
            CompanyName2,
            TaxNo2,
            Website,
            OverseaAddress,
            TransactionalPartnerType.Supplier,
            CurrencyType.USD,
            LocationType.Oversea
        );
        transactionalPartner.Value.UpdateContactPersonInfo(personName, contactInfo);

        result.IsSuccess.Should().BeTrue();
        transactionalPartner.Value.Name.Should().Be(CompanyName2);
        transactionalPartner.Value.TaxNo.Should().Be(TaxNo2);
        transactionalPartner.Value.Website.Should().Be(Website!);
        transactionalPartner.Value.ContactPersonInformation.Name.Should().Be(personName);
        transactionalPartner.Value.ContactPersonInformation.ContactInformation.Should().Be(contactInfo);
        transactionalPartner.Value.Address.Should().Be(OverseaAddress);
        transactionalPartner.Value.TransactionalPartnerType.Should().Be(TransactionalPartnerType.Supplier);
        transactionalPartner.Value.CurrencyType.Should().Be(CurrencyType.USD);
        transactionalPartner.Value.LocationType.Should().Be(LocationType.Oversea);
    }
}