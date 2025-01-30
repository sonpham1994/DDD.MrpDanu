namespace Domain.Services.GetListSupplierIdAndCurrencyTypeIdServices;

public static class GetListSupplierIdAndCurrencyTypeIdService
{
    public static async Task<GetListSupplierIdAndCurrencyTypeIdResult> HandleAsync(
        Guid[] supplierIds,
        Func<IReadOnlyList<Guid>, CancellationToken, Task<IReadOnlyList<SupplierIdWithCurrencyTypeId>>> getListSupplierIdsWithCurrencyTypeId,
        CancellationToken cancellationToken)
    {
        var lstSupplierIdAndCurrencyTypeId = await getListSupplierIdsWithCurrencyTypeId(supplierIds, cancellationToken);
        var result = lstSupplierIdAndCurrencyTypeId.Select(x=> (SupplierIdWithCurrencyType)x).ToArray();
        
        return GetListSupplierIdAndCurrencyTypeIdResult.Success(result);
    }
}