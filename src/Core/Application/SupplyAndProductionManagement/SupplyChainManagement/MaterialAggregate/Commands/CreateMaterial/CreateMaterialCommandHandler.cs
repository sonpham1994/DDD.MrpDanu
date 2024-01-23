using Application.Interfaces;
using Application.Interfaces.Messaging;
using Domain.SupplyChainManagement.MaterialAggregate;
using Domain.SupplyChainManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;
using Domain.SharedKernel.Base;
using Application.Interfaces.Writes.MaterialWrite;
using Application.Interfaces.Writes.TransactionalPartnerWrite;

namespace Application.SupplyChainManagement.MaterialAggregate.Commands.CreateMaterial;

internal sealed class CreateMaterialCommandHandler(
    IUnitOfWork _unitOfWork,
    ITransactionalPartnerQueryForWrite _transactionalPartnerQueryForWrite,
    IMaterialRepository _materialRepository,
    IMaterialQueryForWrite _materialQueryForWrite) : ICommandHandler<CreateMaterialCommand>, ITransactionalCommandHandler
{
    public async Task<Result> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
    {
        var materialType = MaterialType.FromId(request.MaterialTypeId).Value;
        var regionalMarket = RegionalMarket.FromId(request.RegionalMarketId).Value;
        var materialAttributes = MaterialAttributes
            .Create(request.ColorCode, request.Width, request.Weight, request.Unit, request.Varian).Value;
        var uniqueCodeResult = await UniqueMaterialCodeService
            .CheckUniqueMaterialCodeAsync
            (
                request.Code,
                _materialQueryForWrite.GetByCodeAsync,
                cancellationToken
            );
        var material = Material.Create(request.Code, request.Name, materialAttributes, materialType, regionalMarket,
            uniqueCodeResult);
        if (material.IsFailure)
            return material.Error;
        
        _materialRepository.Save(material.Value);

        if (request.MaterialCosts.Any())
        {
            var supplierIds = request.MaterialCosts.Select(x => x.SupplierId).ToList();
            var supplierIdWithCurrencyTypeIds = (await _transactionalPartnerQueryForWrite.GetSupplierIdsWithCurrencyTypeIdBySupplierIdsAsync(supplierIds, cancellationToken))
                .ToTuple();

            var materialCostInputs = request.MaterialCosts.ToTuple();

            var materialSupplierCosts = MaterialSupplierCost.Create(material.Value.Id, materialCostInputs, supplierIdWithCurrencyTypeIds);
            if (materialSupplierCosts.IsFailure)
                return materialSupplierCosts.Error;

            var result = material.Value.UpdateCost(materialSupplierCosts.Value);
            if (result.IsFailure)
                return result;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}