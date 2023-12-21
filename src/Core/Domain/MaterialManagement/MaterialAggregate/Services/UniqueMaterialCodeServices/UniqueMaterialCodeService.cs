using Domain.SharedKernel.Base;
using Domain.SharedKernel.ValueObjects;

namespace Domain.MaterialManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;

//Because domain service is stateless, what this means is we should always be able to simply create a new instance
// of a service to perform an operation, rather than having to rely on any previous history that might have occurred
// within a particular service instance we can use domain service as scoped as per request or just use it as
// static class
public static class UniqueMaterialCodeService
{
    /*
     * There are multiple ways to to check uniqueness data or some situations similar related to making a call to
     * out-of-process dependencies like database
     * please check here: https://github.com/ardalis/DDD-NoDuplicates?tab=readme-ov-file
     */
    public static async Task<Result> CheckUniqueMaterialCodeAsync(
        Material material, 
        Func<string, CancellationToken, Task<IReadOnlyList<MaterialIdWithCode>>> getMaterialByCode, 
        CancellationToken cancellationToken)
    {
        var materialIdWithCodes = await getMaterialByCode.Invoke(material.Code, cancellationToken);
        var existsCodeInAnotherMaterial = materialIdWithCodes
            .FirstOrDefault(x => x.Id != material.Id.Value && material.Code == x.Code);
        
        if (existsCodeInAnotherMaterial is not null)
        {
            return DomainErrors.Material.ExistedCode(material.Code, new MaterialId(existsCodeInAnotherMaterial.Id));
        }

        return Result.Success();
    }
}