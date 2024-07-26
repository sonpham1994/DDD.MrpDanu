namespace SharedKernel.Exceptions;

public sealed class SafeFailResultDomainException() 
    : DomainException(DomainErrors.ResultSafeFail);

public sealed class SafeFailWithCodeResultDomainException(string code) 
    : DomainException(DomainErrors.ResultSafeFailWithCode(code));