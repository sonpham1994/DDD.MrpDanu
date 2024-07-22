using SharedKernel;

namespace Domain.SupplyAndProductionManagement.ProductionPlanning;

public class Unit : ValueObject<Unit>
{
    private static readonly IReadOnlyCollection<decimal> ValidUnitDecimals = new[]
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

    public static Result<Unit?> Create(decimal value)
    {
        if (value <= 0 || (!decimal.IsInteger(value) && !ValidUnitDecimals.Contains(value))) 
            return DomainErrors.BoMRevisionMaterial.InvalidUnit;

        return new Unit(value);
    }

    //decimal convert to IComparable -> value type convert to interface -> boxing? Please check Benchmarks\Benchmark\ValueObjectEqualsBoxing
    protected override IEnumerable<int> GetHashCodeComponents()
    {
        yield return Value.GetHashCode();
    }

    protected override bool EqualComponents(Unit other) 
        => Value == other.Value;
}