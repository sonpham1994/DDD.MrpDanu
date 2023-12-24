using Application.Interfaces;
using Application.Interfaces.Messaging;
using Domain.MaterialManagement.MaterialAggregate;
using Domain.MaterialManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;
using Domain.SharedKernel.Base;
using Application.Interfaces.Writes.MaterialWrite;
using Application.Interfaces.Writes.TransactionalPartnerWrite;

namespace Application.MaterialManagement.MaterialAggregate.Commands.CreateMaterial;

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
        var material = Material.Create(request.Code, request.Name, materialAttributes, materialType, regionalMarket).Value;
        var uniqueCodeResult = await UniqueMaterialCodeService
            .CheckUniqueMaterialCodeAsync
            (
                material,
                _materialQueryForWrite.GetByCodeAsync,
                cancellationToken
            );
        if (uniqueCodeResult.IsFailure)
            return uniqueCodeResult;

        _materialRepository.Save(material);

        if (request.MaterialCosts.Any())
        {
            var supplierIds = request.MaterialCosts.Select(x => x.SupplierId).ToList();
            var supplierIdWithCurrencyTypeIds = (await _transactionalPartnerQueryForWrite.GetSupplierIdsWithCurrencyTypeIdBySupplierIdsAsync(supplierIds, cancellationToken))
                .ToTuple();

            var materialCostInputs = request.MaterialCosts.ToTuple();

            var materialSupplierCosts = MaterialSupplierCost.Create(material.Id, materialCostInputs, supplierIdWithCurrencyTypeIds);
            if (materialSupplierCosts.IsFailure)
                return materialSupplierCosts.Error;

            var result = material.UpdateCost(materialSupplierCosts.Value);
            if (result.IsFailure)
                return result;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}