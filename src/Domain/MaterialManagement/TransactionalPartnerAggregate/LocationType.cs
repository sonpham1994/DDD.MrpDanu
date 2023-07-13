using Domain.Errors;
using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement.TransactionalPartnerAggregate;

public class LocationType : Enumeration<LocationType>
{
    public static readonly LocationType Oversea = new(1, nameof(Oversea));
    public static readonly LocationType Domestic = new(2, nameof(Domestic));
        
    protected LocationType() { }

    private LocationType(byte id, string name) : base(id, name) { }

    public new static Result<LocationType> FromId(byte id)
    {
        var result = Enumeration<LocationType>.FromId(id);
        if (result.IsFailure)
            return DomainErrors.LocationType.NotFoundId(id);

        return result;
    }
}