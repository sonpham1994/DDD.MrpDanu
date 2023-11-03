using Application.MaterialManagement.MaterialAggregate;
using Application.MaterialManagement.MaterialAggregate.Queries.MaterialQueries.GetMaterialById;
using Application.MaterialManagement.MaterialAggregate.Queries.MaterialQueries.GetMaterials;
using Application.MaterialManagement.Shared;
using Application.MaterialManagement.TransactionalPartnerAggregate;
using Application.MaterialManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;
using Application.MaterialManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;
using Domain.MaterialManagement.MaterialAggregate;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel;
using Infrastructure.Persistence.Read.Models;

namespace Infrastructure.Persistence.Read.Extensions;

internal static partial class MaterialManagementExtension
{
    public static MaterialResponse ToMaterialResponse(this MaterialReadModel materialReadModel, IReadOnlyList<MaterialCostManagementResponse> materialCosts)
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
            materialCosts
        );

        return result;
    }

    public static IReadOnlyList<MaterialCostManagementResponse> ToMaterialCostManagementResponse(
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

    public static IReadOnlyList<MaterialsResponse> ToMaterialsResponse(this IEnumerable<MaterialsReadModel> materialsReadModel)
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
    
    public static IReadOnlyList<SuppliersResponse> ToSuppliersResponse(this IEnumerable<SuppliersReadModel> suppliersReadModel)
        => suppliersReadModel.Select(x => new SuppliersResponse(x.Id, x.Name, CurrencyType.FromId(x.CurrencyTypeId).Value.Name)).ToList();

    public static IReadOnlyList<TransactionalPartnersResponse> AsTransactionalPartnersResponse(
        this IEnumerable<TransactionalPartnersReadModel> transactionalPartnersReadModel)
        => transactionalPartnersReadModel
            .Select(x
                => new TransactionalPartnersResponse(x.Id,
                    x.Name,
                    x.TaxNo,
                    x.Website,
                    TransactionalPartnerType.FromId(x.TransactionalPartnerTypeId).Value.Name,
                    CurrencyType.FromId(x.CurrencyTypeId).Value.Name
                )).ToList();

    public static TransactionalPartnerResponse? ToTransactionalPartnerResponse(
        this TransactionalPartnerReadModel? transactionalPartnerReadModel)
    {
        TransactionalPartnerResponse? result = null;
        if (transactionalPartnerReadModel is null)
            return result;

        var transactionalPartnerTypeResponse = TransactionalPartnerType
            .FromId(transactionalPartnerReadModel.TransactionalPartnerTypeId).Value
            .ToResponse();
        var currencyTypeResponse = CurrencyType
            .FromId(transactionalPartnerReadModel.CurrencyTypeId).Value
            .ToResponse();
        var countryResponse = Country
            .FromId(transactionalPartnerReadModel.CountryId).Value
            .ToResponse();
        var locationTypeResponse = LocationType
            .FromId(transactionalPartnerReadModel.LocationTypeId).Value
            .ToResponse();
        var addressResponse = new AddressResponse
            (
                transactionalPartnerReadModel.Address_City,
                transactionalPartnerReadModel.Address_District,
                transactionalPartnerReadModel.Address_Street,
                transactionalPartnerReadModel.Address_Ward,
                transactionalPartnerReadModel.Address_ZipCode,
                countryResponse
            );
        var contactInfoResponse = new ContactPersonInformationResponse
            (
                transactionalPartnerReadModel.ContactPersonName, 
                transactionalPartnerReadModel.TelNo, 
                transactionalPartnerReadModel.Email
            );

        result = new TransactionalPartnerResponse
        (
            transactionalPartnerReadModel.Id,
            transactionalPartnerReadModel.Name,
            transactionalPartnerReadModel.TaxNo,
            transactionalPartnerReadModel.Website,
            transactionalPartnerTypeResponse,
            currencyTypeResponse,
            addressResponse,
            contactInfoResponse,
            locationTypeResponse
        );

        return result;
    }
}