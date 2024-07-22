using Application.Interfaces;
using Application.Interfaces.Messaging;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;
using SharedKernel;
using Application.Interfaces.Writes.MaterialWrite;
using Application.Interfaces.Writes.TransactionalPartnerWrite;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

namespace Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.CreateMaterial;

internal sealed class CreateMaterialCommandHandler(
    IUnitOfWork unitOfWork,
    ITransactionalPartnerReadonlyRepository transactionalPartnerReadonlyRepository,
    IMaterialRepository materialRepository,
    IMaterialReadonlyRepository materialReadonlyRepository) : ICommandHandler<CreateMaterialCommand>, ITransactionalCommandHandler
{
    public async Task<Result> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
    {
        var materialType = MaterialType.FromId(request.MaterialTypeId).Value!;
        var regionalMarket = RegionalMarket.FromId(request.RegionalMarketId).Value!;
        var materialAttributes = MaterialAttributes
            .Create(request.ColorCode, request.Width, request.Weight, request.Unit, request.Varian).Value!;
        var code = MaterialCode.Create(request.Code).Value!;
        var name = MaterialName.Create(request.Name).Value!;
        var uniqueCodeResult = await UniqueMaterialCodeService
            .CheckUniqueMaterialCodeAsync
            (
                code,
                materialReadonlyRepository.GetByCodeAsync,
                cancellationToken
            );
        var material = Material.Create(code, name, materialAttributes, materialType, regionalMarket,
            uniqueCodeResult);
        if (!material.IsSuccess)
            return material.Error;

        materialRepository.Save(material.Value!);

        if (request.MaterialCosts?.Count > 0)
        {
            var supplierIds = request.MaterialCosts.Select(x => x.SupplierId).ToList();
            var supplierIdWithCurrencyTypeIds = (await transactionalPartnerReadonlyRepository.GetSupplierIdsWithCurrencyTypeIdBySupplierIdsAsync(supplierIds, cancellationToken))
                .ToTuple();

            var materialCostInputs = request.MaterialCosts.ToTuple();

            var materialSupplierCosts = MaterialSupplierCost.Create(material.Value!.Id, materialCostInputs, supplierIdWithCurrencyTypeIds);
            if (!materialSupplierCosts.IsSuccess)
                return materialSupplierCosts.Error;

            var result = material.Value.UpdateCost(materialSupplierCosts.Value!);
            if (!result.IsSuccess)
                return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}