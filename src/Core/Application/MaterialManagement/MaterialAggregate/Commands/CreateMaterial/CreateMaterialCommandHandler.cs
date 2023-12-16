using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Messaging;
using Domain.MaterialManagement.MaterialAggregate;
using Domain.Services;
using Domain.SharedKernel.Base;

namespace Application.MaterialManagement.MaterialAggregate.Commands.CreateMaterial;

internal sealed class CreateMaterialCommandHandler : ICommandHandler<CreateMaterialCommand>, ITransactionalCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionalPartnerRepository _transactionalPartnerRepository;
    private readonly IMaterialRepository _materialRepository;

    public CreateMaterialCommandHandler(IUnitOfWork unitOfWork,
        ITransactionalPartnerRepository transactionalPartnerRepository,
        IMaterialRepository materialRepository)
    {
        _unitOfWork = unitOfWork;
        _transactionalPartnerRepository = transactionalPartnerRepository;
        _materialRepository = materialRepository;
    }
    
    public async Task<Result> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
    {
        var materialType = MaterialType.FromId(request.MaterialTypeId).Value;
        var regionalMarket = RegionalMarket.FromId(request.RegionalMarketId).Value;
        var materialAttributes = MaterialAttributes
            .Create(request.ColorCode, request.Width, request.Weight, request.Unit, request.Varian).Value;
        var material = Material.Create(request.Code, request.Name, materialAttributes, materialType, regionalMarket).Value;
        var materialsByCode = await _materialRepository.GetByCodeAsync(request.Code, cancellationToken);
        var uniqueCodeResult = UniqueMaterialCode.CheckUniqueMaterialCode(material, materialsByCode);
        if (uniqueCodeResult.IsFailure)
            return uniqueCodeResult;
        
        if (request.MaterialCosts.Any())
        {
            var suppliers = await _transactionalPartnerRepository.GetByIdsAsync(request.MaterialCosts.Select(x => x.SupplierId).ToList(), cancellationToken);

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
        }
        
        _materialRepository.Save(material);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}