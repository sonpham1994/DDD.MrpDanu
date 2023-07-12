using Domain.SharedKernel.Base;

namespace Web.ApiModels.BaseResponses;

public sealed record AppError
{
    public string Code { get; }
    public string Message { get; }

    public AppError(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public static implicit operator AppError(DomainError domainError)
    {
        return new AppError(domainError.Code, domainError.Message);
    }
}

public static class AppErrors
{
    public static AppError InternalServerError(string message) => new("InternalServerError", message);
}