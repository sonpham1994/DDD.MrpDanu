using SharedKernel;
using SharedKernel.Exceptions;
using SharedKernel.Extensions;

namespace Domain.Services.GetListSupplierIdAndCurrencyTypeIdServices;

public readonly struct GetListSupplierIdAndCurrencyTypeIdResult : IResult<IReadOnlyList<SupplierIdWithCurrencyType>>
{
    private readonly bool _isSuccess;
    private readonly DomainError _error;
    private readonly IReadOnlyList<SupplierIdWithCurrencyType> _value;

    private GetListSupplierIdAndCurrencyTypeIdResult(in bool isSuccess, in DomainError error, IReadOnlyList<SupplierIdWithCurrencyType> value)
    {
        _isSuccess = isSuccess;
        _error = error;
        this.CheckSafeFailResult(_isSuccess, _error);
        _value = value;
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

    public IReadOnlyList<SupplierIdWithCurrencyType> Value
    {
        get
        {
            if (!_isSuccess)
                throw new SafeFailWithCodeResultDomainException(_error.Code);

            return _value;
        }
    }

    internal static GetListSupplierIdAndCurrencyTypeIdResult Success(IReadOnlyList<SupplierIdWithCurrencyType> value)
        => new(true, DomainError.Empty, value);
}