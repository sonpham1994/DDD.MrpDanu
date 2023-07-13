using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Messaging;
using Domain.Errors;
using Domain.MaterialManagement.MaterialAggregate;
using Domain.SharedKernel.Base;
using Domain.MaterialManagement.TransactionalPartnerAggregate;

namespace Application.MaterialManagement.MaterialAggregate.Commands.UpdateMaterial;

internal sealed class UpdateMaterialCommandHandler : ITransactionalCommandHandler<UpdateMaterialCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionalPartnerRepository _transactionalPartnerRepository;
    private readonly IMaterialRepository _materialRepository;

    public UpdateMaterialCommandHandler(IUnitOfWork unitOfWork,
        ITransactionalPartnerRepository transactionalPartnerRepository,
        IMaterialRepository materialRepository)
    {
        _unitOfWork = unitOfWork;
        _transactionalPartnerRepository = transactionalPartnerRepository;
        _materialRepository = materialRepository;
    }

    public async Task<Result> Handle(UpdateMaterialCommand request, CancellationToken cancellationToken)
    {
        var material = await _materialRepository.GetByIdAsync(request.Id, cancellationToken);
        if (material is null)
            return DomainErrors.Material.MaterialIdNotFound(request.Id);

        var suppliers = await _transactionalPartnerRepository.GetByIdsAsync(request.MaterialCosts.Select(x => x.SupplierId).ToList(), cancellationToken);

        var regionalMarket = RegionalMarket.FromId(request.RegionalMarketId).Value;
        var materialType = MaterialType.FromId(request.MaterialTypeId).Value;
        var materialAttributes = MaterialAttributes.Create(request.Name, request.ColorCode, request.Width,
            request.Weight, request.Unit, request.Varian).Value;

        var materialResult = material.UpdateMaterial(request.Code, materialAttributes, materialType, regionalMarket);
        if (materialResult.IsFailure)
            return materialResult;

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
