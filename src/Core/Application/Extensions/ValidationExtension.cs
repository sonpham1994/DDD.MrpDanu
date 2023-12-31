using Domain.SharedKernel.Base;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Extensions;

internal static class ValidationExtension
{
    public static IReadOnlyList<DomainError> ToDomainErrors(this IEnumerable<ValidationFailure> failures)
    {
        //should consider how to convert to DomainError to use internal constructor for DomainError
        return failures.Select(x => new DomainError(x.ErrorCode, x.ErrorMessage)).ToList();
    }

    public static ValidationFailure ToValidationFailure(this in DomainError domainError)
        => new()
        {
            ErrorCode = domainError.Code,
            ErrorMessage = domainError.Message
        };

    public static IRuleBuilderOptions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueObject>> factoryMethod)
        where TValueObject : ValueObject
    {
        return (IRuleBuilderOptions<T, TElement>)ruleBuilder.Custom((value, context) =>
        {
            Result<TValueObject> result = factoryMethod(value);

            if (result.IsFailure)
            {
                context.AddFailure(result.Error.ToValidationFailure());
            }
        });
    }

    public static IRuleBuilderOptions<T, TElement> MustBeEntity<T, TElement, TEntity>(
       this IRuleBuilder<T, TElement> ruleBuilder,
       Func<TElement, Result<TEntity>> factoryMethod)
       where TEntity : Entity
    {
        return (IRuleBuilderOptions<T, TElement>)ruleBuilder.Custom((value, context) =>
        {
            Result<TEntity> result = factoryMethod(value);

            if (result.IsFailure)
            {
                context.AddFailure(result.Error.ToValidationFailure());
            }
        });
    }
    
    public static IRuleBuilderOptions<T, TElement> MustBeEntityGuidStronglyTypedId<T, TElement, TEntity, TId>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TEntity>> factoryMethod)
        where TEntity : EntityGuidStronglyTypedId<TId>
        where TId : struct, IEquatable<TId>, IGuidStronglyTypedId
    {
        return (IRuleBuilderOptions<T, TElement>)ruleBuilder.Custom((value, context) =>
        {
            Result<TEntity> result = factoryMethod(value);

            if (result.IsFailure)
            {
                context.AddFailure(result.Error.ToValidationFailure());
            }
        });
    }

    public static IRuleBuilderOptions<T, TElement> MustBeEnumeration<T, TElement, TEnumeration>(
       this IRuleBuilder<T, TElement> ruleBuilder,
       Func<TElement, Result<TEnumeration>> factoryMethod)
       where TEnumeration : Enumeration<TEnumeration>
    {
        return (IRuleBuilderOptions<T, TElement>)ruleBuilder.Custom((value, context) =>
        {
            Result<TEnumeration> result = factoryMethod(value);

            if (result.IsFailure)
            {
                context.AddFailure(result.Error.ToValidationFailure());
            }
        });
    }
}