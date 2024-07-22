namespace SharedKernel.Exceptions;

public sealed class DomainException : Exception
{
    public IReadOnlyList<DomainError> Errors { get; }

    public DomainException(IReadOnlyList<DomainError> errors) => Errors = errors;
    
    public DomainException(in DomainError error) => Errors = new DomainError[] {error};
}