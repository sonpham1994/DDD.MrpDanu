using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.CreateMaterial;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.DeleteMaterial;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Commands.UpdateMaterial;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterialById;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterials;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Commands.CreateTransactionalPartner;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Commands.DeleteTransactionalPartner;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Commands.UpdateTransactionalPartner;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetSuppliers;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;
using MediatR;
using MinimalApi.ApiResponses;

namespace MinimalApi.Endpoints;

public class SupplyChainManagement : IEndpoint
{
    private const string GetMaterialByIdRouteName = "GetMaterialById";
    private const string GetTransactionalPartnerByIdRouteName = "GetTransactionalPartnerById";
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/supply-chain-management");

        MapMaterialEndpoints(group);
        MapTransactionalPartnerEndpoints(group);
    }

    private IEndpointRouteBuilder MapMaterialEndpoints(IEndpointRouteBuilder group)
    {
        var materialGroup = group.MapGroup("materials");

        materialGroup.MapPost("", async (CreateMaterialCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);

            return result.ToResultApiAcceptedOrBadRequest(GetMaterialByIdRouteName);
        });

        materialGroup.MapGet("", async (CancellationToken cancellationToken, ISender sender) =>
        {
            var materials = await sender.Send(new GetMaterialsQuery(), cancellationToken);

            return materials.ToResultApiSuccess();
        });

        materialGroup.MapGet("{id:guid}", async (Guid id, CancellationToken cancellationToken, ISender sender) =>
            {
                var materialResult = await sender.Send(new GetMaterialByIdQuery(id), cancellationToken);

                return materialResult.ToResultApiSuccessOrBadRequest();
            })
            .WithName(GetMaterialByIdRouteName);

        materialGroup.MapPut("{id:guid}",
            async (UpdateMaterialCommand request, CancellationToken cancellationToken, ISender sender) =>
            {
                var result = await sender.Send(request, cancellationToken);

                return result.ToResultApiSuccessOrBadRequest();
            });

        materialGroup.MapDelete("{id:guid}", async (Guid id, CancellationToken cancellationToken, ISender sender) =>
        {
            var result = await sender.Send(new DeleteMaterialCommand(id), cancellationToken);

            return result.ToResultApiSuccessOrBadRequest();
        });

        return group;
    }

    private void MapTransactionalPartnerEndpoints(IEndpointRouteBuilder group)
    {
        var materialGroup = group.MapGroup("transactional-partners");

        materialGroup.MapGet("", async (CancellationToken cancellationToken, ISender sender) =>
        {
            var transactionalPartnersResult = await sender.Send(new GetTransactionalPartnersQuery(), cancellationToken);

            return transactionalPartnersResult.ToResultApiSuccess();
        });

        materialGroup.MapGet("suppliers", async (CancellationToken cancellationToken, ISender sender) =>
        {
            var suppliersResult = await sender.Send(new GetSuppliersQuery(), cancellationToken);

            return suppliersResult.ToResultApiSuccess();
        });

        materialGroup.MapGet("{id:guid}", async (Guid id, CancellationToken cancellationToken, ISender sender) =>
        {
            var materialResult = await sender.Send(new GetTransactionalPartnerByIdQuery(id), cancellationToken);

            return materialResult.ToResultApiSuccessOrBadRequest();
        })
        .WithName(GetTransactionalPartnerByIdRouteName);

        materialGroup.MapPost("", async (CreateTransactionalPartnerCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);

            return result.ToResultApiAcceptedOrBadRequest(GetTransactionalPartnerByIdRouteName);
        });

        materialGroup.MapPut("{id:guid}",
            async (UpdateTransactionalPartnerCommand request, CancellationToken cancellationToken, ISender sender) =>
            {
                var result = await sender.Send(request, cancellationToken);

                return result.ToResultApiSuccessOrBadRequest();
            });

        materialGroup.MapDelete("{id:guid}", async (Guid id, CancellationToken cancellationToken, ISender sender) =>
        {
            var result = await sender.Send(new DeleteTransactionalPartnerCommand(id), cancellationToken);

            return result.ToResultApiSuccessOrBadRequest();
        });
    }
}