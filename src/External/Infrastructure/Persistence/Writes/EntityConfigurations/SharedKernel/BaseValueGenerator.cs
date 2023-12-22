using Domain.SharedKernel.Base;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Infrastructure.Persistence.Write.EntityConfigurations.SharedKernel;

internal abstract class GuidStronglyTypedIdValueGenerator<TId> : ValueGenerator<TId>
    where TId : struct, IEquatable<TId>, IGuidStronglyTypedId
{
    private static readonly SequentialGuidValueGenerator _generator = new();
    
    public override bool GeneratesTemporaryValues => false;

    public override TId Next(EntityEntry entry)
    {
        return Next(_generator.Next(null));
    }

    protected abstract TId Next(Guid id);
}