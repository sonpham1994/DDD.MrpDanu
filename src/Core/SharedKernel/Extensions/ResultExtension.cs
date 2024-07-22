using SharedKernel.Exceptions;

namespace SharedKernel.Extensions;

public static class ResultExtension
{
    public static void CheckSafeFailResult<T>(this T _, in bool isSuccess, in DomainError error)
        where T : IResult
    {
        if (!isSuccess && error.IsEmpty())
            throw new DomainException(DomainErrors.SafeFail);
    }
}