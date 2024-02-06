using Domain.SharedKernel.Base;
using Domain.SupplyAndProductionManagement.SupplyChainManagement;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;

public class LocationType : Enumeration<LocationType>
{
    public static readonly LocationType Oversea = new(1, nameof(Oversea));
    public static readonly LocationType Domestic = new(2, nameof(Domestic));

    protected LocationType() { }

    private LocationType(in byte id, string name) : base(id, name) { }

    public new static Result<LocationType> FromId(in byte id)
    {
        var result = Enumeration<LocationType>.FromId(id);
        if (result.IsFailure)
            return DomainErrors.LocationType.NotFoundId(id);

        return result;
    }
}