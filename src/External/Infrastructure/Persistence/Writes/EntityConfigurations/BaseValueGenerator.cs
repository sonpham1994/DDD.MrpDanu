using SharedKernel;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Infrastructure.Persistence.Writes.EntityConfigurations;

internal abstract class GuidStronglyTypedIdValueGenerator<TId> : ValueGenerator<TId>
    where TId : IEquatable<TId>, IGuidStronglyTypedId
{
    private static readonly SequentialGuidValueGenerator _generator = new();

    public override bool GeneratesTemporaryValues => false;

    public override TId Next(EntityEntry entry)
    {
        return Next(_generator.Next(default!));
    }

    protected abstract TId Next(Guid id);
}