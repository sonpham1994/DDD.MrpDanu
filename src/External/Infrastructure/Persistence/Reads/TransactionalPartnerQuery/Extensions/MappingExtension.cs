using Application.SupplyChainManagement.Shared;
using Application.SupplyChainManagement.TransactionalPartnerAggregate;
using Application.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartnerById;
using Application.SupplyChainManagement.TransactionalPartnerAggregate.Queries.GetTransactionalPartners;
using Domain.SupplyChainManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel.Enumerations;
using Infrastructure.Persistence.Reads.TransactionalPartnerQuery.Models;

namespace Infrastructure.Persistence.Reads.TransactionalPartnerQuery.Extensions;

// the reason why we put the query in extensions class is that, we can reuse the projection from another place,
// reduce duplication projection. So other methods in TransactionalPartnerQuery can reuse this projection to
// do their own business
// please check https://www.youtube.com/watch?v=bnTxWV99qdE&t=562s&ab_channel=MilanJovanovi%C4%87
//https://www.youtube.com/watch?v=xPMlz9c2xIU&list=PL9hNzBRaTninA0iIildslO4UmxHXSGrat&index=3&ab_channel=NickChapsas
internal static class MappingExtension
{
    public static IReadOnlyList<SuppliersResponse> ToResponse(this IEnumerable<SuppliersReadModel> suppliersReadModel)
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

    public static TransactionalPartnerResponse? ToResponse(
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