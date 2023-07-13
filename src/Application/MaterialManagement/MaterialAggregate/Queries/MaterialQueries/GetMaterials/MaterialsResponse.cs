namespace Application.MaterialManagement.MaterialAggregate.Queries.MaterialQueries.GetMaterials;

public sealed record MaterialsResponse(Guid Id, 
    string Code, 
    string Name, 
    string ColorCode, 
    string Width, 
    string Weight, 
    string Unit, 
    string Varian, 
    string RegionalMarketName, 
    string MaterialTypeName);