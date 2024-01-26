using Domain.SharedKernel.Exceptions;

namespace Domain.SharedKernel.Base;

internal static class Extensions
{
    public static void CheckSafeFailResult<T>(this T _, bool isSuccess, DomainError error)
        where T : IResult
    {
        if (!isSuccess && error.IsEmpty())
            throw new DomainException(DomainErrors.SafeFail);
    }
}