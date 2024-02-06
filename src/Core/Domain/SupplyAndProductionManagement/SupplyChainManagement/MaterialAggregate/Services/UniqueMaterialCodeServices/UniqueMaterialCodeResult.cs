using Domain.SharedKernel.Base;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;

public readonly struct UniqueMaterialCodeResult : IResult
{
    private readonly bool _isSuccess;
    private readonly DomainError _error;
    private UniqueMaterialCodeResult(bool isSuccess, in DomainError error)
    {
        _isSuccess = isSuccess;
        _error = error;
        this.CheckSafeFailResult(_isSuccess, _error);
    }

    public bool IsFailure => !IsSuccess;

    public bool IsSuccess
    {
        get
        {
            this.CheckSafeFailResult(_isSuccess, _error);
            return _isSuccess;
        }
    }

    public DomainError Error
    {
        get
        {
            this.CheckSafeFailResult(_isSuccess, _error);
            return _error;
        }
    }

    public static implicit operator UniqueMaterialCodeResult(in DomainError error)
    {
        if (error.IsEmpty())
            return new(true, error);
        else
            return new(false, error);

    }

    public static implicit operator UniqueMaterialCodeResult(in Result result)
    {
        return new(result.IsSuccess, result.Error);
    }
}