using Domain.SharedKernel.ValueObjects;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;

namespace Domain.Tests.SupplyAndProductionManagement.SupplyChainManagement;

public static class SupplyChainManagementPreparingData
{
    public static readonly CompanyName CompanyName1 = CompanyName.Create("CompanyName").Value;
    public static readonly TaxNo TaxNo1 = TaxNo.Create("0123456789", Country.VietNam).Value;
    public static readonly Address Address1 = Address.Create("abc", "xyz", "aaa", "bbb", "12345", Country.VietNam).Value;
    public static readonly CompanyName CompanyName2 = CompanyName.Create("CompanyName2").Value;
    public static readonly TaxNo TaxNo2 = TaxNo.Create("1111111111", Country.VietNam).Value;
    public static readonly Address OverseaAddress = Address.Create("abc", "xyz", "aaa", "bbb", "12345", Country.US).Value;
    public static readonly Website? Website = Website.Create("http://abcxyz.com").Value;
    public static MaterialId MaterialId1 => (MaterialId)Guid.Parse("579e2ec4-707b-4a70-d04f-08dc0f30d4b0");
    public static MaterialId MaterialId2 => (MaterialId)Guid.Parse("42ed74e9-b341-495d-6b1d-08dc0f820746");
    public static SupplierId SupplierId1 => (SupplierId)Guid.Parse("9c3fa05b-12a4-4927-c977-08dc24a71752");
    public static SupplierId SupplierId2 => (SupplierId)Guid.Parse("ef384330-f467-4537-c978-08dc24a71752");
    public static readonly PersonName PersonName1 = PersonName.Create("Name1").Value;
    public static readonly ContactInformation ContactInformation1 = ContactInformation.Create("1234567890", string.Empty).Value;

    public static readonly MaterialAttributes MaterialAttributes1 =
        MaterialAttributes.Create("colorCode1", "width1", "weight1", "unit1", "varian1").Value;
}