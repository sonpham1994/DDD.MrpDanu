using Application.Interfaces;
using Application.Interfaces.Messaging;
using Domain.MaterialManagement.MaterialAggregate;
using Domain.MaterialManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;
using Domain.SharedKernel.Base;
using Domain.SharedKernel.ValueObjects;
using Application.Interfaces.Reads;
using Application.Interfaces.Writes.MaterialWrite;

namespace Application.MaterialManagement.MaterialAggregate.Commands.CreateMaterial;

internal sealed class CreateMaterialCommandHandler : ICommandHandler<CreateMaterialCommand>, ITransactionalCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionalPartnerQuery _transactionalPartnerQuery;
    private readonly IMaterialRepository _materialRepository;
    private readonly IMaterialQuery _materialQuery;

    public CreateMaterialCommandHandler(IUnitOfWork unitOfWork,
        ITransactionalPartnerQuery transactionalPartnerQuery,
        IMaterialRepository materialRepository,
        IMaterialQuery materialQuery)
    {
        _unitOfWork = unitOfWork;
        _transactionalPartnerQuery = transactionalPartnerQuery;
        _materialRepository = materialRepository;
        _materialQuery = materialQuery;
    }
    
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
                (code, cancelToken) => _materialQuery.GetByCodeAsync(code, cancelToken), 
                cancellationToken
            );
        if (uniqueCodeResult.IsFailure)
            return uniqueCodeResult;

        _materialRepository.Save(material);
        
        if (request.MaterialCosts.Any())
        {
            var supplierIds = request.MaterialCosts.Select(x => x.SupplierId).ToList();
            var supplierIdWithCurrencyTypeIds = (await _transactionalPartnerQuery.GetSupplierIdsWithCurrencyTypeIdBySupplierIdsAsync(supplierIds, cancellationToken))
                .Select(x => x.ToTuple())
                .ToList();
            
            var materialCostInputs = request.MaterialCosts
                .Select(x => x.ToTuple())
                .ToList();
            
            var materialSupplierCosts = MaterialSupplierCost.Create(material.Id, materialCostInputs, supplierIdWithCurrencyTypeIds);
            if (materialSupplierCosts.IsFailure)
                return materialSupplierCosts.Error;
        
            // var result = material.UpdateCost(materialCosts.Value);
            // if (result.IsFailure)
            //     return result;
        }
        
        //_materialRepository.Save(material);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}