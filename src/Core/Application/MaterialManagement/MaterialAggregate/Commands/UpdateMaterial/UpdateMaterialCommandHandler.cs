using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Messaging;
using Application.Interfaces.Queries;
using Domain.MaterialManagement.MaterialAggregate;
using Domain.Services.UniqueMaterialCodeService;
using Domain.SharedKernel.Base;
using DomainErrors = Domain.MaterialManagement.DomainErrors;

namespace Application.MaterialManagement.MaterialAggregate.Commands.UpdateMaterial;

internal sealed class UpdateMaterialCommandHandler : ICommandHandler<UpdateMaterialCommand>, ITransactionalCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionalPartnerRepository _transactionalPartnerRepository;
    private readonly IMaterialRepository _materialRepository;
    private readonly IMaterialQuery _materialQuery;

    public UpdateMaterialCommandHandler(IUnitOfWork unitOfWork,
        ITransactionalPartnerRepository transactionalPartnerRepository,
        IMaterialRepository materialRepository, IMaterialQuery materialQuery)
    {
        _unitOfWork = unitOfWork;
        _transactionalPartnerRepository = transactionalPartnerRepository;
        _materialRepository = materialRepository;
        _materialQuery = materialQuery;
    }

    public async Task<Result> Handle(UpdateMaterialCommand request, CancellationToken cancellationToken)
    {
        var material = await _materialRepository.GetByIdAsync(request.Id, cancellationToken);
        if (material is null)
            return DomainErrors.Material.MaterialIdNotFound(request.Id);

        var suppliers = await _transactionalPartnerRepository.GetByIdsAsync(request.MaterialCosts.Select(x => x.SupplierId).ToList(), cancellationToken);

        var regionalMarket = RegionalMarket.FromId(request.RegionalMarketId).Value;
        var materialType = MaterialType.FromId(request.MaterialTypeId).Value;
        var materialAttributes = MaterialAttributes.Create(request.ColorCode, request.Width,
            request.Weight, request.Unit, request.Varian).Value;

        var materialResult = material.UpdateMaterial(request.Code, request.Name, materialAttributes, materialType, regionalMarket);
        if (materialResult.IsFailure)
            return materialResult;
        var uniqueCodeResult = await UniqueMaterialCode
            .CheckUniqueMaterialCodeAsync
            (
                material, 
                (code, cancelToken) => _materialQuery.GetByCodeAsync(code, cancelToken), 
                cancellationToken
            );
        if (uniqueCodeResult.IsFailure)
            return uniqueCodeResult;

        var input = request.MaterialCosts
            .Where(x => x is not null)
            .Select(x => (x.Price, x.MinQuantity, x.Surcharge, x.SupplierId))
            .ToList();
        var materialCosts = MaterialCostManagement.Create(input, suppliers);
        if (materialCosts.IsFailure)
            return materialCosts.Error;

        var result = material.UpdateCost(materialCosts.Value);
        if (result.IsFailure)
            return result;

        //_materialRepository.Save(material);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
