using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

namespace Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

public static class Extensions
{
    public static MaterialTypeResponse ToResponse(this MaterialType materialType)
        => new(materialType.Id, materialType.Name);
    
    public static RegionalMarketResponse ToResponse(this RegionalMarket regionalMarket)
        => new(regionalMarket.Id, regionalMarket.Name);
}