using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Messaging;
using Application.Interfaces.Queries;
using Domain.MaterialManagement.MaterialAggregate;
using Domain.MaterialManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;
using Domain.SharedKernel.Base;
using Domain.SharedKernel.ValueObjects;

namespace Application.MaterialManagement.MaterialAggregate.Commands.CreateMaterial;

internal sealed class CreateMaterialCommandHandler : ICommandHandler<CreateMaterialCommand>, ITransactionalCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionalPartnerRepository _transactionalPartnerRepository;
    private readonly IMaterialRepository _materialRepository;
    private readonly IMaterialQuery _materialQuery;

    public CreateMaterialCommandHandler(IUnitOfWork unitOfWork,
        ITransactionalPartnerRepository transactionalPartnerRepository,
        IMaterialRepository materialRepository,
        IMaterialQuery materialQuery)
    {
        _unitOfWork = unitOfWork;
        _transactionalPartnerRepository = transactionalPartnerRepository;
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
        
        // if (request.MaterialCosts.Any())
        // {
        //     var suppliers = await _transactionalPartnerRepository
        //         .GetSuppliersByIdsAsync
        //             (request.MaterialCosts.Select(x => new SupplierId(x.SupplierId)).ToList(), cancellationToken);
        //
        //     var materialCosts = request.MaterialCosts
        //         .Where(x => x is not null)
        //         .Select(x => (x.Price, x.MinQuantity, x.Surcharge, MaterialCost.Create(material.Id, new SupplierId(x.SupplierId), Money.Create(x.Price).Value)))
        //         .ToList();
        //     
        //     var input = request.MaterialCosts
        //         .Where(x => x is not null)
        //         .Select(x => (x.Price, x.MinQuantity, x.Surcharge, MaterialCost.Create(material.Id, new SupplierId(x.SupplierId), Money.Create(x.Price).Value)))
        //         .ToList();
        //     var materialSupplierCosts = MaterialSupplierCost.Create(input, suppliers);
        //     if (materialCosts.IsFailure)
        //         return materialCosts.Error;
        //
        //     var result = material.UpdateCost(materialCosts.Value);
        //     if (result.IsFailure)
        //         return result;
        // }
        
        _materialRepository.Save(material);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}