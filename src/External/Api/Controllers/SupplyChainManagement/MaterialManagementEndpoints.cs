using Api.ApiResponses;
using Application.SupplyChainManagement.MaterialAggregate;
using Application.SupplyChainManagement.TransactionalPartnerAggregate;
using Domain.SupplyChainManagement.MaterialAggregate;
using Domain.SupplyChainManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel.Enumerations;

namespace Api.Controllers.SupplyChainManagement;

//https://www.youtube.com/watch?v=gsAuFIhXz3g&ab_channel=MilanJovanovi%C4%87
//https://www.youtube.com/watch?v=GCuVC_qDOV4&ab_channel=MilanJovanovi%C4%87
public static class SupplyChainManagementEndpoints
{
    public static void MapSupplyChainManagementEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("material-management");

        group.MapGet("material-types", () =>
        {
            IReadOnlyList<MaterialTypeResponse> result = MaterialType.List
                .Select(x => x.ToResponse())
                .ToList();
            return ApiResponseExtensions<IReadOnlyList<MaterialTypeResponse>>.Success(result);
        });
        
        group.MapGet("regional-markets", () =>
        {
            IReadOnlyList<RegionalMarketResponse> result = RegionalMarket.List
                .Select(x => x.ToResponse())
                .ToList();
            return ApiResponseExtensions<IReadOnlyList<RegionalMarketResponse>>.Success(result);
        });
        
        group.MapGet("countries", () =>
        {
            IReadOnlyList<CountryResponse> countries = Country.List
                .Select(x => x.ToResponse())
                .ToList();
            return ApiResponseExtensions<IReadOnlyList<CountryResponse>>.Success(countries);
        });
        
        group.MapGet("location-types", () =>
        {
            IReadOnlyList<LocationTypeResponse> result = LocationType.List
                .Select(x => x.ToResponse())
                .ToList();
            return ApiResponseExtensions<IReadOnlyList<LocationTypeResponse>>.Success(result);
        });
        
        group.MapGet("transactional-partner-types", () =>
        {
            IReadOnlyList<TransactionalPartnerTypeResponse> result = TransactionalPartnerType.List
                .Select(x => x.ToResponse())
                .ToList();
            return ApiResponseExtensions<IReadOnlyList<TransactionalPartnerTypeResponse>>.Success(result);
        });
        
        group.MapGet("currency-types", () =>
        {
            IReadOnlyList<CurrencyTypeResponse> result = CurrencyType.List
                .Select(x => x.ToResponse())
                .ToList();
            return ApiResponseExtensions<IReadOnlyList<CurrencyTypeResponse>>.Success(result);
        });
    }
}