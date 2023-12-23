using Application.Interfaces;
using Application.Interfaces.Messaging;
using Domain.MaterialManagement.MaterialAggregate;
using Domain.MaterialManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;
using Domain.SharedKernel.Base;
using Application.Interfaces.Writes.MaterialWrite;
using Application.Interfaces.Writes.TransactionalPartnerWrite;
using Domain.SharedKernel.ValueObjects;
using DomainErrors = Domain.MaterialManagement.DomainErrors;

namespace Application.MaterialManagement.MaterialAggregate.Commands.UpdateMaterial;

internal sealed class UpdateMaterialCommandHandler : ICommandHandler<UpdateMaterialCommand>, ITransactionalCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionalPartnerQueryForWrite _transactionalPartnerQueryForWrite;
    private readonly IMaterialRepository _materialRepository;
    private readonly IMaterialQueryForWrite _materialQueryForWrite;

    public UpdateMaterialCommandHandler(IUnitOfWork unitOfWork,
        ITransactionalPartnerQueryForWrite transactionalPartnerQueryForWrite,
        IMaterialRepository materialRepository,
        IMaterialQueryForWrite materialQueryForWrite)
    {
        _unitOfWork = unitOfWork;
        _transactionalPartnerQueryForWrite = transactionalPartnerQueryForWrite;
        _materialRepository = materialRepository;
        _materialQueryForWrite = materialQueryForWrite;
    }

    public async Task<Result> Handle(UpdateMaterialCommand request, CancellationToken cancellationToken)
    {
        var material = await _materialRepository.GetByIdAsync((MaterialId)request.Id, cancellationToken);
        if (material is null)
            return DomainErrors.Material.MaterialIdNotFound(request.Id);

        var regionalMarket = RegionalMarket.FromId(request.RegionalMarketId).Value;
        var materialType = MaterialType.FromId(request.MaterialTypeId).Value;
        var materialAttributes = MaterialAttributes.Create(request.ColorCode, request.Width,
            request.Weight, request.Unit, request.Varian).Value;

        var materialResult = material.UpdateMaterial(request.Code, request.Name, materialAttributes, materialType, regionalMarket);
        if (materialResult.IsFailure)
            return materialResult;
        var uniqueCodeResult = await UniqueMaterialCodeService
            .CheckUniqueMaterialCodeAsync
            (
                material, 
                (code, cancelToken) => _materialQueryForWrite.GetByCodeAsync(code, cancelToken), 
                cancellationToken
            );
        if (uniqueCodeResult.IsFailure)
            return uniqueCodeResult;

        var materialCostInputs = request.MaterialCosts
            .ToTuple();
        var supplierIds = request.MaterialCosts.Select(x => x.SupplierId).ToList();
        var supplierIdWithCurrencyTypeIds = (await _transactionalPartnerQueryForWrite.GetSupplierIdsWithCurrencyTypeIdBySupplierIdsAsync(supplierIds, cancellationToken))
            .ToTuple();
        
        var materialSupplierCosts = MaterialSupplierCost.Create(material.Id, materialCostInputs, supplierIdWithCurrencyTypeIds);
        if (materialSupplierCosts.IsFailure)
            return materialSupplierCosts.Error;

        var result = material.UpdateCost(materialSupplierCosts.Value);
        if (result.IsFailure)
            return result;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
