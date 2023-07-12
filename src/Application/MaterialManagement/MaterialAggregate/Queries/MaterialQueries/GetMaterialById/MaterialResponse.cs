namespace Application.MaterialManagement.MaterialAggregate.Queries.MaterialQueries.GetMaterialById;

public sealed record MaterialResponse(Guid Id, 
    string Code, 
    string Name, 
    string ColorCode, 
    string Width, 
    string Weight, 
    string Unit, 
    string Varian, 
    MaterialTypeResponse MaterialType, 
    RegionalMarketResponse RegionalMarket, 
    IReadOnlyList<MaterialCostManagementResponse> MaterialCostManagements);