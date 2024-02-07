using Domain.SharedKernel.Base;
using Domain.SharedKernel.ValueObjects;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;

public readonly struct UniqueMaterialCodeResult : IResult
{
    private readonly bool _isSuccess;
    private readonly DomainError _error;
    private UniqueMaterialCodeResult(in bool isSuccess, in DomainError error)
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

    internal static UniqueMaterialCodeResult Failure(string code, MaterialId materialId)
        => new(false, DomainErrors.Material.ExistedCode(code, materialId));

    internal static UniqueMaterialCodeResult Success()
        => new(true, DomainError.Empty);
}