using Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;

namespace Infrastructure.Persistence.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartners;
internal static class Extensions
{
    public static IReadOnlyList<byte> GetSupplierTypeIds()
    {
        return TransactionalPartnerType.GetSupplierTypes().ToArray().Select(x => x.Id).ToList();
    }
}