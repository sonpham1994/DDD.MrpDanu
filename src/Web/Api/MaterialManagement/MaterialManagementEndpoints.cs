using Application.MaterialManagement.MaterialAggregate;
using Application.MaterialManagement.MaterialAggregate.Queries.MaterialQueries.GetMaterialById;
using Application.MaterialManagement.Shared;
using Application.MaterialManagement.TransactionalPartnerAggregate.Queries.GetSuppliers;
using Domain.MaterialManagement.MaterialAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.ApiModels.BaseResponses;

namespace Web.Api.MaterialManagement;

//https://www.youtube.com/watch?v=gsAuFIhXz3g&ab_channel=MilanJovanovi%C4%87
//https://www.youtube.com/watch?v=GCuVC_qDOV4&ab_channel=MilanJovanovi%C4%87
public static class MaterialManagementEndpoints
{
    public static void MapMaterialManagementEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/material-management");

        group.MapGet("material-types", () =>
        {
            IReadOnlyList<MaterialTypeResponse> materialTypes = MaterialType.List
                .Select(x => x.ToResponse())
                .ToList();
            return AppResponse<IReadOnlyList<MaterialTypeResponse>>.Success(materialTypes);
        });
        
        group.MapGet("regional-markets", () =>
        {
            IReadOnlyList<RegionalMarketResponse> regionalMarkets = RegionalMarket.List
                .Select(x => x.ToResponse())
                .ToList();
            return AppResponse<IReadOnlyList<RegionalMarketResponse>>.Success(regionalMarkets);
        });
        
        group.MapGet("suppliers", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var suppliers = await sender.Send(new GetSuppliersQuery(), cancellationToken);

            return AppResponse<IReadOnlyList<SuppliersResponse>>.Success(suppliers.Value);
        });
    }
}