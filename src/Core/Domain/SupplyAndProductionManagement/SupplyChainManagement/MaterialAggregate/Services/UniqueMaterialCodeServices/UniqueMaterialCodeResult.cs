using Domain.SharedKernel.Base;

namespace Domain.SupplyChainManagement.MaterialAggregate.Services.UniqueMaterialCodeServices;

public readonly struct UniqueMaterialCodeResult
{
    private UniqueMaterialCodeResult(bool isSuccess, in DomainError error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }
    public bool IsSuccess { get; }
    public DomainError Error { get; }

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