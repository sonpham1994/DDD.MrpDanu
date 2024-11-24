using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.CreateMaterial;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.DeleteMaterial;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.UpdateMaterial;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterialById;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterials;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Commands.CreateTransactionalPartner;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Commands.DeleteTransactionalPartner;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Commands.UpdateTransactionalPartner;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetSuppliers;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
using MediatR;
using MinimalApi.ApiResponses;
using SharedKernel.Enumerations;

namespace MinimalApi.Endpoints;

public static class SupplyChainManagement
{
    public static IEndpointRouteBuilder MapSupplyChainManagementEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("supply-chain-management");

        group.MapCommonSupplyChainManagementEndpoints()
            .MapMaterialEndpoints()
            .MapTransactionalPartnerEndpoints();

        return group;
    }
    
    private static IEndpointRouteBuilder MapMaterialEndpoints(this IEndpointRouteBuilder group)
    {
        var materialGroup = group.MapGroup("materials");
        materialGroup.MapPost("", async (CreateMaterialCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);

            return result.IsSuccess 
                ? Results.Ok(ApiResponseExtensions.ToApiSuccess())
                : Results.BadRequest(result.Error.ToApiFailure());
        });
        
        materialGroup.MapGet("", async (CancellationToken cancellationToken, ISender sender) =>
        {
            var materials = await sender.Send(new GetMaterialsQuery(), cancellationToken);
            
            return Results.Ok(materials.Value.ToApiSuccess());
        });
        
        materialGroup.MapGet("{id:guid}", async (Guid id, CancellationToken cancellationToken, ISender sender) =>
        {
            var materialResult = await sender.Send(new GetMaterialByIdQuery(id), cancellationToken);

            return materialResult.IsSuccess ? Results.Ok(materialResult.Value.ToApiSuccess()) : Results.BadRequest(materialResult.Error.ToApiFailure());
        });
        
        materialGroup.MapPut("{id:guid}", async (UpdateMaterialCommand request, CancellationToken cancellationToken, ISender sender) =>
        {
            var result = await sender.Send(request, cancellationToken);

            return result.IsSuccess ? Results.Ok(ApiResponseExtensions.ToApiSuccess()) : Results.BadRequest(result.Error.ToApiFailure());
        });
        
        materialGroup.MapDelete("{id:guid}", async (Guid id, CancellationToken cancellationToken, ISender sender) =>
        {
            var result = await sender.Send(new DeleteMaterialCommand(id), cancellationToken);

            return result.IsSuccess ? Results.Ok(ApiResponseExtensions.ToApiSuccess()) : Results.BadRequest(result.Error.ToApiFailure());
        });
        

        return group;
    }
    
    private static IEndpointRouteBuilder MapTransactionalPartnerEndpoints(this IEndpointRouteBuilder group)
    {
        var materialGroup = group.MapGroup("transactional-partners");
        
        materialGroup.MapGet("", async (CancellationToken cancellationToken, ISender sender) =>
        {
            var materials = await sender.Send(new GetTransactionalPartnersQuery(), cancellationToken);
            
            return Results.Ok(materials.Value.ToApiSuccess());
        });
        
        materialGroup.MapGet("suppliers", async (CancellationToken cancellationToken, ISender sender) =>
        {
            var materials = await sender.Send(new GetSuppliersQuery(), cancellationToken);
            
            return Results.Ok(materials.Value.ToApiSuccess());
        });
        
        materialGroup.MapGet("{id:guid}", async (Guid id, CancellationToken cancellationToken, ISender sender) =>
        {
            var materialResult = await sender.Send(new GetTransactionalPartnerByIdQuery(id), cancellationToken);

            return materialResult.IsSuccess ? Results.Ok(materialResult.Value.ToApiSuccess()) : Results.BadRequest(materialResult.Error.ToApiFailure());
        });
        
        materialGroup.MapPost("", async (CreateTransactionalPartnerCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);

            return result.IsSuccess 
                ? Results.Ok(ApiResponseExtensions.ToApiSuccess())
                : Results.BadRequest(result.Error.ToApiFailure());
        });
        
        materialGroup.MapPut("{id:guid}", async (UpdateTransactionalPartnerCommand request, CancellationToken cancellationToken, ISender sender) =>
        {
            var result = await sender.Send(request, cancellationToken);

            return result.IsSuccess ? Results.Ok(ApiResponseExtensions.ToApiSuccess()) : Results.BadRequest(result.Error.ToApiFailure());
        });
        
        materialGroup.MapDelete("{id:guid}", async (Guid id, CancellationToken cancellationToken, ISender sender) =>
        {
            var result = await sender.Send(new DeleteTransactionalPartnerCommand(id), cancellationToken);

            return result.IsSuccess ? Results.Ok(ApiResponseExtensions.ToApiSuccess()) : Results.BadRequest(result.Error.ToApiFailure());
        });

        return group;
    }

    private static IEndpointRouteBuilder MapCommonSupplyChainManagementEndpoints(this IEndpointRouteBuilder group)
    {
        group.MapGet("material-types", () =>
        {
            IReadOnlyList<MaterialTypeResponse> result = MaterialType.List
                .Select(x => x.ToResponse())
                .ToList();
            
            return result.ToApiSuccess();
        });

        group.MapGet("regional-markets", () =>
        {
            IReadOnlyList<RegionalMarketResponse> result = RegionalMarket.List
                .Select(x => x.ToResponse())
                .ToList();
            
            return result.ToApiSuccess();
        });

        group.MapGet("countries", () =>
        {
            IReadOnlyList<CountryResponse> result = Country.List
                .Select(x => x.ToResponse())
                .ToList();
            
            return result.ToApiSuccess();
        });

        group.MapGet("location-types", () =>
        {
            IReadOnlyList<LocationTypeResponse> result = LocationType.List
                .Select(x => x.ToResponse())
                .ToList();
            
            return result.ToApiSuccess();
        });

        group.MapGet("transactional-partner-types", () =>
        {
            IReadOnlyList<TransactionalPartnerTypeResponse> result = TransactionalPartnerType.List
                .Select(x => x.ToResponse())
                .ToList();
            
            return result.ToApiSuccess();
        });

        group.MapGet("currency-types", () =>
        {
            IReadOnlyList<CurrencyTypeResponse> result = CurrencyType.List
                .Select(x => x.ToResponse())
                .ToList();
            
            return result.ToApiSuccess();
        });

        return group;
    }
}