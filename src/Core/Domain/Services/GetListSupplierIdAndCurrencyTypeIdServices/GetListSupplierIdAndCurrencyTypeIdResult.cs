using SharedKernel;
using SharedKernel.Extensions;

namespace Domain.Services.GetListSupplierIdAndCurrencyTypeIdServices;

public readonly struct GetListSupplierIdAndCurrencyTypeIdResult : IResult<IReadOnlyList<SupplierIdWithCurrencyType>>
{
    private readonly bool _isSuccess;
    private readonly DomainError _error;

    private GetListSupplierIdAndCurrencyTypeIdResult(in bool isSuccess, in DomainError error, IReadOnlyList<SupplierIdWithCurrencyType> value)
    {
        _isSuccess = isSuccess;
        _error = error;
        this.CheckSafeFailResult(_isSuccess, _error);
        Value = value;
    }

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
    
    public IReadOnlyList<SupplierIdWithCurrencyType> Value { get; }
    
    internal static GetListSupplierIdAndCurrencyTypeIdResult Success(IReadOnlyList<SupplierIdWithCurrencyType> value)
        => new(true, DomainError.Empty, value);
}