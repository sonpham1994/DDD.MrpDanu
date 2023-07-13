using Domain.Errors;
using Domain.SharedKernel.Base;

namespace Domain.ProductManagement;

public class Unit : ValueObject
{
    private static readonly IReadOnlyCollection<decimal> validUnitDecimals = new List<decimal>
    {
        0.3m, 
        0.003m, 
        1.5m, 
        0.002m, 
        0.5m, 
        0.05m,
        0.005m
    };

    public decimal Value { get; }

    private Unit(decimal value)
    {
        Value = value;
    }

    public static Result<Unit> Create(decimal value)
    {
        if (value <= 0 || (!decimal.IsInteger(value) && !validUnitDecimals.Contains(value))) 
            return DomainErrors.BoMRevisionMaterial.InvalidUnit;

        return new Unit(value);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}