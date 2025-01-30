using Application.Interfaces;
using Application.Interfaces.Messaging;
using SharedKernel;
using Application.Interfaces.Writes.MaterialWrite;
using Application.Interfaces.Writes.Shared;
using Domain.Services.GetListSupplierIdAndCurrencyTypeIdServices;
using Domain.Services.UniqueMaterialCodeServices;
using SharedKernel.ValueObjects;
using DomainErrors = Domain.SupplyAndProductionManagement.SupplyChainManagement.DomainErrors;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

namespace Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.UpdateMaterial;

internal sealed class UpdateMaterialCommandHandler(
    IUnitOfWork unitOfWork,
    ISharedTransactionalPartnerReadonlyRepository transactionalPartnerReadonlyRepository,
    IMaterialRepository materialRepository,
    IMaterialReadonlyRepository materialReadonlyRepository) : ICommandHandler<UpdateMaterialCommand>
{
    public async Task<Result> Handle(UpdateMaterialCommand request, CancellationToken cancellationToken)
    {
        var material = await materialRepository.GetByIdAsync((MaterialId)request.Id, cancellationToken);
        if (material is null)
            return DomainErrors.Material.NotFoundId(request.Id);

        var regionalMarket = RegionalMarket.FromId(request.RegionalMarketId).Value!;
        var materialType = MaterialType.FromId(request.MaterialTypeId).Value!;
        var materialAttributes = MaterialAttributes.Create(request.ColorCode, request.Width!,
            request.Weight, request.Unit, request.Varian).Value!;
        var code = MaterialCode.Create(request.Code).Value!;
        var name = MaterialName.Create(request.Name).Value!;

        var uniqueCodeResult = await CheckUniqueMaterialCodeService
            .HandleAsync
            (
                material.Id,
                code,
                materialReadonlyRepository.GetByCodeAsync,
                cancellationToken
            );
        var materialResult = material.UpdateMaterial(code, name, materialAttributes, materialType, regionalMarket, uniqueCodeResult);
        if (!materialResult.IsSuccess)
            return materialResult;

        var materialCostInputs = request.MaterialCosts
            .ToTuple();
        var supplierIds = request.MaterialCosts.Select(x => x.SupplierId).ToArray();
        var getListSupplierIdAndCurrencyTypeIdResult = await GetListSupplierIdAndCurrencyTypeIdService
            .HandleAsync(supplierIds, transactionalPartnerReadonlyRepository.GetSupplierIdsWithCurrencyTypeIdBySupplierIdsAsync, cancellationToken);

        var materialSupplierCosts = MaterialSupplierCost.Create(material.Id, materialCostInputs, getListSupplierIdAndCurrencyTypeIdResult);
        if (!materialSupplierCosts.IsSuccess)
            return materialSupplierCosts.Error;

        var result = material.UpdateCost(materialSupplierCosts.Value!);
        if (!result.IsSuccess)
            return result;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
