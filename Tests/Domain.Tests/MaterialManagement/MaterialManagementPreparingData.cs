using Domain.MaterialManagement.MaterialAggregate;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel;

namespace Domain.Tests.MaterialManagement;

public static class MaterialManagementPreparingData
{
    public static CompanyName CompanyName1 => CompanyName.Create("CompanyName").Value;
    public static TaxNo TaxNo1 => TaxNo.Create("0123456789", Country.VietNam).Value;
    public static Address Address1 => Address.Create("abc", "xyz", "aaa", "bbb", "12345", Country.VietNam).Value;
    public static CompanyName CompanyName2 => CompanyName.Create("CompanyName2").Value;
    public static TaxNo TaxNo2 => TaxNo.Create("1111111111", Country.VietNam).Value;
    public static Address Address2 => Address.Create("abc2", "xyz2", "aaa2", "bbb2", "45678", Country.VietNam).Value;
    public static Address OverseaAddress => Address.Create("abc", "xyz", "aaa", "bbb", "12345", Country.US).Value;
    public static Website? Website => Website.Create("http://abcxyz.com").Value;
    public static readonly Guid TransactionalPartnerId1 = new("da4f04e5-3db9-4c3d-aa0e-8a2c52046184");
    public static readonly Guid TransactionalPartnerId2 = new("fec76123-5d6d-442b-a4a9-0b6269d6204d");
    public static PersonName PersonName1 => PersonName.Create("Name1").Value;
    public static ContactInformation ContactInformation1 => ContactInformation.Create("1234567890", string.Empty).Value;
    public static ContactPersonInformation ContactPersonInfo1 => new(PersonName.Create("Name1").Value
        , ContactInformation.Create("0123456789", string.Empty).Value);
    public static ContactPersonInformation ContactPersonInfo2 => new(PersonName.Create("Name2").Value
        , ContactInformation.Create("1234567890", string.Empty).Value);

    public static MaterialAttributes MaterialAttributes1 =>
        MaterialAttributes.Create("name1", "colorCode1", "width1", "weight1", "unit1", "varian1").Value;
    
    public static MaterialAttributes MaterialAttributes2 =>
        MaterialAttributes.Create("name2", "colorCode2", "width2", "weight2", "unit2", "varian2").Value;

    public static TransactionalPartner TransactionalPartnerWithCustomerType => TransactionalPartner.Create(
        CompanyName1,
        TaxNo1,
        Website,
        PersonName1,
        ContactInformation1,
        Address1,
        TransactionalPartnerType.Customer,
        CurrencyType.VND,
        LocationType.Domestic
    ).Value;
    
    public static TransactionalPartner TransactionalPartnerWithSupplierType => TransactionalPartner.Create(
        CompanyName1,
        TaxNo1,
        Website,
        PersonName1,
        ContactInformation1,
        Address1,
        TransactionalPartnerType.Supplier,
        CurrencyType.VND,
        LocationType.Domestic
    ).Value;
}