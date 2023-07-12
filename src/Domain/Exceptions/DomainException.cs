using Domain.SharedKernel.Base;

namespace Domain.Exceptions;

public sealed class DomainException : Exception
{
    public IReadOnlyList<DomainError> Errors { get; }

    public DomainException(IReadOnlyList<DomainError> errors)
    {
        Errors = errors;
    }
    
    public DomainException(DomainError error)
    {
        Errors = new List<DomainError>(1){error};
    }
}