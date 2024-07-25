namespace SharedKernel.Exceptions;

public sealed class SafeFailResultDomainException() : DomainException(DomainErrors.SafeFail);