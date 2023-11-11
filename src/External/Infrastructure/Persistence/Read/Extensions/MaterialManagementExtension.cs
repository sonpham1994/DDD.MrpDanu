using Domain.MaterialManagement.TransactionalPartnerAggregate;

namespace Infrastructure.Persistence.Read.Extensions;

//https://www.youtube.com/watch?v=xPMlz9c2xIU&list=PL9hNzBRaTninA0iIildslO4UmxHXSGrat&index=3&ab_channel=NickChapsas
internal static partial class MaterialManagementExtension
{
    public static IReadOnlyList<byte> GetSupplierTypeIds()
    {
        return TransactionalPartnerType.GetSupplierTypes().ToArray().Select(x => x.Id).ToList();
    }
}