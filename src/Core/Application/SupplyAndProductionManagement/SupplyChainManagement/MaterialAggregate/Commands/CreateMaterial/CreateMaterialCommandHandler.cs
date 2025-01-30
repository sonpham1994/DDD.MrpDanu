using Application.Interfaces;
using Application.Interfaces.Messaging;
using SharedKernel;
using Application.Interfaces.Writes.MaterialWrite;
using Application.Interfaces.Writes.Shared;
using Domain.Services.GetListSupplierIdAndCurrencyTypeIdServices;
using Domain.Services.UniqueMaterialCodeServices;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using SharedKernel.ValueObjects;

namespace Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.CreateMaterial;

internal sealed class CreateMaterialCommandHandler(
    IUnitOfWork unitOfWork,
    ISharedTransactionalPartnerReadonlyRepository transactionalPartnerReadonlyRepository,
    IMaterialRepository materialRepository,
    IMaterialReadonlyRepository materialReadonlyRepository) 
        : ICommandHandler<CreateMaterialCommand, MaterialId>
{
    public async Task<Result<MaterialId>> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
    {
        var materialType = MaterialType.FromId(request.MaterialTypeId).Value!;
        var regionalMarket = RegionalMarket.FromId(request.RegionalMarketId).Value!;
        var materialAttributes = MaterialAttributes
            .Create(request.ColorCode, request.Width, request.Weight, request.Unit, request.Varian).Value!;
        var code = MaterialCode.Create(request.Code).Value!;
        var name = MaterialName.Create(request.Name).Value!;
        var uniqueCodeResult = await CheckUniqueMaterialCodeService
            .HandleAsync
            (
                code,
                materialReadonlyRepository.GetByCodeAsync,
                cancellationToken
            );
        var materialResult = Material.Create(code, name, materialAttributes, materialType, regionalMarket,
            uniqueCodeResult);
        if (!materialResult.IsSuccess)
            return materialResult.Error;

        var material = materialResult.Value!;
        materialRepository.Save(material);

        if (request.MaterialCosts.Count > 0)
        {
            var supplierIds = request.MaterialCosts.Select(x => x.SupplierId).ToArray();
            var getListSupplierIdAndCurrencyTypeIdResult = await GetListSupplierIdAndCurrencyTypeIdService
                .HandleAsync(supplierIds, transactionalPartnerReadonlyRepository.GetSupplierIdsWithCurrencyTypeIdBySupplierIdsAsync, cancellationToken);
            var materialCostInputs = request.MaterialCosts.ToTuple();

            var materialSupplierCosts = MaterialSupplierCost.Create(materialResult.Value!.Id, materialCostInputs, getListSupplierIdAndCurrencyTypeIdResult);
            if (!materialSupplierCosts.IsSuccess)
                return materialSupplierCosts.Error;

            var result = materialResult.Value.UpdateCost(materialSupplierCosts.Value!);
            if (!result.IsSuccess)
                return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return material.Id;
    }
}