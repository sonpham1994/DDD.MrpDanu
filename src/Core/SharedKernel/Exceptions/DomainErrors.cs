namespace SharedKernel.Exceptions;

public class DomainErrors
{
    public static DomainError SafeFail => new("SafeFail", "DomainError cannot null if process is fail");
}