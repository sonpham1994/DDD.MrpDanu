using SharedKernel.Enumerations;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
using Shouldly;
using DomainErrors = Domain.SupplyAndProductionManagement.SupplyChainManagement.DomainErrors;

namespace Domain.Tests.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
using static SupplyChainManagementPreparingData;

public class TransactionalPartnerTests
{
    [Fact]
    public void Cannot_create_transactional_partner_if_location_is_oversea_and_country_is_vietnam()
    {
        var result = TransactionalPartner.Create
        (
            CompanyName1,
            TaxNo1,
            SWebsite,
            PersonName1,
            ContactInformation1,
            Address1,
            TransactionalPartnerType.Both,
            CurrencyType.USD,
            LocationType.Oversea
        );

        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBe(DomainErrors.TransactionalPartner.InvalidCountryAndLocationType);
    }

    [Fact]
    public void Cannot_create_transactional_partner_if_location_is_domestic_and_country_is_not_vietnam()
    {
        var result = TransactionalPartner.Create
        (
            CompanyName1,
            TaxNo1,
            SWebsite,
            PersonName1,
            ContactInformation1,
            OverseaAddress,
            TransactionalPartnerType.Both,
            CurrencyType.USD,
            LocationType.Domestic
        );

        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBe(DomainErrors.TransactionalPartner.InvalidCountryAndLocationType);
    }

    [Fact]
    public void Cannot_create_transactional_partner_if_country_is_vietnam_and_currency_is_not_vnd()
    {
        var result = TransactionalPartner.Create
        (
            CompanyName1,
            TaxNo1,
            SWebsite,
            PersonName1,
            ContactInformation1,
            Address1,
            TransactionalPartnerType.Both,
            CurrencyType.USD,
            LocationType.Domestic
        );

        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBe(DomainErrors.TransactionalPartner.InvalidCurrencyType);
    }

    [Fact]
    public void Create_transactional_partner_successfully()
    {
        var result = TransactionalPartner.Create
        (
            CompanyName1,
            TaxNo1,
            SWebsite,
            PersonName1,
            ContactInformation1,
            Address1,
            TransactionalPartnerType.Both,
            CurrencyType.VND,
            LocationType.Domestic
        );

        result.IsSuccess.ShouldBeTrue();
        result.Value!.Name.ShouldBe(CompanyName1);
        result.Value.TaxNo.ShouldBe(TaxNo1);
        result.Value.Website.ShouldBe(Website!);
        result.Value.ContactPersonInformation.Name.ShouldBe(PersonName1);
        result.Value.ContactPersonInformation.ContactInformation.ShouldBe(ContactInformation1);
        result.Value.Address.ShouldBe(Address1);
        result.Value.TransactionalPartnerType.ShouldBe(TransactionalPartnerType.Both);
        result.Value.CurrencyType.ShouldBe(CurrencyType.VND);
        result.Value.LocationType.ShouldBe(LocationType.Domestic);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Create_transactional_partner_successfully_with_null_or_empty_website(string? website)
    {
        var result = TransactionalPartner.Create
        (
            CompanyName1,
            TaxNo1,
            website,
            PersonName1,
            ContactInformation1,
            Address1,
            TransactionalPartnerType.Both,
            CurrencyType.VND,
            LocationType.Domestic
        );

        result.IsSuccess.ShouldBeTrue();
        result.Value!.Website.ShouldBeNull();
    }

    [Fact]
    public void Cannot_update_transactional_partner_if_location_is_oversea_and_country_is_vietnam()
    {
        var transactionalPartner = TransactionalPartner.Create
        (
            CompanyName1,
            TaxNo1,
            SWebsite,
            PersonName1,
            ContactInformation1,
            Address1,
            TransactionalPartnerType.Both,
            CurrencyType.VND,
            LocationType.Domestic
        );

        var result = transactionalPartner.Value!.Update
        (
            CompanyName1,
            TaxNo1,
            SWebsite,
            Address1,
            TransactionalPartnerType.Both,
            CurrencyType.USD,
            LocationType.Oversea
        );

        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBe(DomainErrors.TransactionalPartner.InvalidCountryAndLocationType);
    }

    [Fact]
    public void Cannot_update_transactional_partner_if_location_is_domestic_and_country_is_not_vietnam()
    {
        var transactionalPartner = TransactionalPartner.Create
        (
            CompanyName1,
            TaxNo1,
            SWebsite,
            PersonName1,
            ContactInformation1,
            Address1,
            TransactionalPartnerType.Both,
            CurrencyType.VND,
            LocationType.Domestic
        );

        var result = transactionalPartner.Value!.Update
        (
            CompanyName1,
            TaxNo1,
            SWebsite,
            OverseaAddress,
            TransactionalPartnerType.Both,
            CurrencyType.USD,
            LocationType.Domestic
        );

        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBe(DomainErrors.TransactionalPartner.InvalidCountryAndLocationType);
    }

    [Fact]
    public void Cannot_update_transactional_partner_if_country_is_vietnam_and_currency_is_not_vnd()
    {
        var transactionalPartner = TransactionalPartner.Create
        (
            CompanyName1,
            TaxNo1,
            SWebsite,
            PersonName1,
            ContactInformation1,
            Address1,
            TransactionalPartnerType.Both,
            CurrencyType.VND,
            LocationType.Domestic
        );

        var result = transactionalPartner.Value!.Update
        (
            CompanyName1,
            TaxNo1,
            SWebsite,
            Address1,
            TransactionalPartnerType.Both,
            CurrencyType.USD,
            LocationType.Domestic
        );

        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBe(DomainErrors.TransactionalPartner.InvalidCurrencyType);
    }

    [Fact]
    public void Update_transactional_partner_successfully()
    {
        var transactionalPartner = TransactionalPartner.Create
        (
            CompanyName1,
            TaxNo1,
            SWebsite,
            PersonName1,
            ContactInformation1,
            Address1,
            TransactionalPartnerType.Both,
            CurrencyType.VND,
            LocationType.Domestic
        );

        var personName = PersonName.Create("Name2").Value;
        var contactInfo = ContactInformation.Create(string.Empty, "abcxyz@gmail.com").Value;
        var result = transactionalPartner.Value!.Update
        (
            CompanyName2,
            TaxNo2,
            SWebsite,
            OverseaAddress,
            TransactionalPartnerType.Supplier,
            CurrencyType.USD,
            LocationType.Oversea
        );
        transactionalPartner.Value.UpdateContactPersonInfo(personName!, contactInfo!);

        result.IsSuccess.ShouldBeTrue();
        transactionalPartner.Value.Name.ShouldBe(CompanyName2);
        transactionalPartner.Value.TaxNo.ShouldBe(TaxNo2);
        transactionalPartner.Value.Website.ShouldBe(Website!);
        transactionalPartner.Value.ContactPersonInformation.Name.ShouldBe(personName);
        transactionalPartner.Value.ContactPersonInformation.ContactInformation.ShouldBe(contactInfo);
        transactionalPartner.Value.Address.ShouldBe(OverseaAddress);
        transactionalPartner.Value.TransactionalPartnerType.ShouldBe(TransactionalPartnerType.Supplier);
        transactionalPartner.Value.CurrencyType.ShouldBe(CurrencyType.USD);
        transactionalPartner.Value.LocationType.ShouldBe(LocationType.Oversea);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Update_transactional_partner_successfully_with_null_or_empty_website(string? website)
    {
        var transactionalPartner = TransactionalPartner.Create
        (
            CompanyName1,
            TaxNo1,
            SWebsite,
            PersonName1,
            ContactInformation1,
            Address1,
            TransactionalPartnerType.Both,
            CurrencyType.VND,
            LocationType.Domestic
        );

        var result = transactionalPartner.Value!.Update
        (
            CompanyName1,
            TaxNo1,
            website,
            Address1,
            TransactionalPartnerType.Both,
            CurrencyType.VND,
            LocationType.Domestic
        );

        result.IsSuccess.ShouldBeTrue();
        transactionalPartner.Value.Website.ShouldBeNull();
    }
}