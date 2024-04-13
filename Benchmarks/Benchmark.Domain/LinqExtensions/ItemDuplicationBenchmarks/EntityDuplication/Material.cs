namespace Benchmark.Domain.LinqExtensions.ItemDupplicationBenchmarks.EntityDuplication;

public readonly record struct MaterialId(Guid Value)
{
}
public class Material : EntityGuidStronglyTypedId<MaterialId>
{
    
}