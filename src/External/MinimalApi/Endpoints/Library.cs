using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
using MinimalApi.ApiResponses;
using SharedKernel.Enumerations;

namespace MinimalApi.Endpoints;

public class Library : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/libraries");
        
        group.MapGet("material-types", () =>
        {
            IReadOnlyList<MaterialTypeResponse> result = MaterialType.List
                .Select(x => x.ToResponse())
                .ToList();

            return Results.Ok(result.ToApiSuccess());
        });

        group.MapGet("regional-markets", () =>
        {
            IReadOnlyList<RegionalMarketResponse> result = RegionalMarket.List
                .Select(x => x.ToResponse())
                .ToList();

            return Results.Ok(result.ToApiSuccess());
        });

        group.MapGet("countries", () =>
        {
            IReadOnlyList<CountryResponse> result = Country.List
                .Select(x => x.ToResponse())
                .ToList();

            return Results.Ok(result.ToApiSuccess());
        });

        group.MapGet("location-types", () =>
        {
            IReadOnlyList<LocationTypeResponse> result = LocationType.List
                .Select(x => x.ToResponse())
                .ToList();

            return Results.Ok(result.ToApiSuccess());
        });

        group.MapGet("transactional-partner-types", () =>
        {
            IReadOnlyList<TransactionalPartnerTypeResponse> result = TransactionalPartnerType.List
                .Select(x => x.ToResponse())
                .ToList();

            return Results.Ok(result.ToApiSuccess());
        });

        group.MapGet("currency-types", () =>
        {
            IReadOnlyList<CurrencyTypeResponse> result = CurrencyType.List
                .Select(x => x.ToResponse())
                .ToList();

            return Results.Ok(result.ToApiSuccess());
        });
    }
}