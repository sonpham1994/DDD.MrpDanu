using Domain.SupplyChainManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel.Enumerations;

namespace Application.SupplyChainManagement.TransactionalPartnerAggregate;

public static class Extensions
{
    public static CurrencyTypeResponse ToResponse(this CurrencyType currencyType)
        => new(currencyType.Id, currencyType.Name);

    public static CountryResponse ToResponse(this Country country)
        => new(country.Id, country.Name);

    public static LocationTypeResponse ToResponse(this LocationType locationType)
        => new(locationType.Id, locationType.Name);

    public static TransactionalPartnerTypeResponse ToResponse(
        this TransactionalPartnerType transactionalPartnerType)
        => new(transactionalPartnerType.Id, transactionalPartnerType.Name);
}