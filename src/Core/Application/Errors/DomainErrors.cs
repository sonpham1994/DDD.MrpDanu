using Domain.SharedKernel.Base;

namespace Application.Errors;

internal sealed class DomainErrors
{
    public static DomainError NullHandler => new("Handler.Null", "Handler should not be null.");
}