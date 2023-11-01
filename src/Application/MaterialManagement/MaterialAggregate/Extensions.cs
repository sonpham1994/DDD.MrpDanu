using Application.MaterialManagement.MaterialAggregate.Queries.MaterialQueries.GetMaterialById;
using Domain.MaterialManagement.MaterialAggregate;

namespace Application.MaterialManagement.MaterialAggregate;

public static class Extensions
{
    public static MaterialTypeResponse ToResponse(this MaterialType materialType)
        => new(materialType.Id, materialType.Name);
    
    public static RegionalMarketResponse ToResponse(this RegionalMarket regionalMarket)
        => new(regionalMarket.Id, regionalMarket.Name);
}