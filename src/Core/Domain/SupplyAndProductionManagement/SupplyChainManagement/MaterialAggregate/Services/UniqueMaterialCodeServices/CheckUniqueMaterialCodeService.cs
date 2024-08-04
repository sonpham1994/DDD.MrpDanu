using SharedKernel.ValueObjects;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;

//Because domain service is stateless, what this means is we should always be able to simply create a new instance
// of a service to perform an operation, rather than having to rely on any previous history that might have occurred
// within a particular service instance we can use domain service as scoped as per request or just use it as
// static class
public static class CheckUniqueMaterialCodeService
{
    /*
     * There are multiple ways to to check uniqueness data or some situations similar related to making a call to
     * out-of-process dependencies like database
     * please check here: https://github.com/ardalis/DDD-NoDuplicates?tab=readme-ov-file
     */
    public static async Task<UniqueMaterialCodeResult> CheckUniqueMaterialCodeAsync(
        MaterialCode code,
        Func<MaterialCode, CancellationToken, Task<MaterialIdWithCode?>> getMaterialByCode,
        CancellationToken cancellationToken)
    {
        var materialIdWithCode = await getMaterialByCode.Invoke(code, cancellationToken);

        if (materialIdWithCode is not null)
        {
            return UniqueMaterialCodeResult.Failure(code, materialIdWithCode.MaterialId);
        }

        return UniqueMaterialCodeResult.Success();
    }

    public static async Task<UniqueMaterialCodeResult> CheckUniqueMaterialCodeAsync(
        MaterialId materialId,
        MaterialCode code,
        Func<MaterialCode, CancellationToken, Task<MaterialIdWithCode?>> getMaterialByCode,
        CancellationToken cancellationToken)
    {
        var materialIdWithCode = await getMaterialByCode.Invoke(code, cancellationToken);
        if (materialIdWithCode is not null
            && materialId != materialIdWithCode.MaterialId
            && code.Value == materialIdWithCode.Code)
        {
            return UniqueMaterialCodeResult.Failure(code, materialIdWithCode.MaterialId);
        }

        return UniqueMaterialCodeResult.Success();
    }
}