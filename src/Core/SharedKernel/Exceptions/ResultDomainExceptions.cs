namespace SharedKernel.Exceptions;

public sealed class SafeFailResultDomainException() 
    : DomainException(DomainErrors.SafeFail);

public sealed class SafeFailWithCodeResultDomainException(string code) 
    : DomainException(DomainErrors.SafeFailWithCode(code));