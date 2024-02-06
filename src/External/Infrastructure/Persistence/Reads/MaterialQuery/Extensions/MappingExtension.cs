using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterialById;
using Application.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Queries.GetMaterials;
using Application.SupplyAndProductionManagement.SupplyChainManagement.Shared;
using Domain.SupplyChainManagement.MaterialAggregate;
using Domain.SharedKernel.Enumerations;
using Infrastructure.Persistence.Reads.MaterialQuery.Models;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

namespace Infrastructure.Persistence.Reads.MaterialQuery.Extensions;

//https://www.youtube.com/watch?v=xPMlz9c2xIU&list=PL9hNzBRaTninA0iIildslO4UmxHXSGrat&index=3&ab_channel=NickChapsas
internal static class MappingExtension
{
    public static MaterialResponse ToResponse(this MaterialReadModel materialReadModel)
    {
        var materialTypeResponse = MaterialType
            .FromId(materialReadModel.MaterialTypeId).Value
            .ToResponse();
        var regionalMarketResponse = RegionalMarket
            .FromId(materialReadModel.RegionalMarketId).Value
            .ToResponse();

        var result = new MaterialResponse
        (
            materialReadModel.Id,
            materialReadModel.Code,
            materialReadModel.Name,
            materialReadModel.ColorCode,
            materialReadModel.Width,
            materialReadModel.Weight,
            materialReadModel.Unit,
            materialReadModel.Varian,
            materialTypeResponse,
            regionalMarketResponse,
            materialReadModel.MaterialCosts.ToResponse()
        );

        return result;
    }

    public static IReadOnlyList<MaterialCostManagementResponse> ToResponse(
        this IEnumerable<MaterialCostReadModel> materialCostReadModel)
        => materialCostReadModel.Select(x =>
        {
            string currencyTypeName = CurrencyType.FromId(x.CurrencyTypeId).Value.Name;
            var supplierResponse = new SuppliersResponse(x.SupplierId, x.SupplierName, currencyTypeName);

            return new MaterialCostManagementResponse(
                x.Price,
                x.MinQuantity,
                x.Surcharge,
                supplierResponse);
        }).ToList();

    public static IReadOnlyList<MaterialsResponse> ToResponse(this IEnumerable<MaterialsReadModel> materialsReadModel)
        => materialsReadModel.Select(x => new MaterialsResponse
        (
            x.Id,
            x.Code,
            x.Name,
            x.ColorCode,
            x.Width,
            x.Weight,
            x.Unit,
            x.Varian,
            RegionalMarket.FromId(x.RegionalMarketId).Value.Name,
            MaterialType.FromId(x.MaterialTypeId).Value.Name
        )).ToList();


}