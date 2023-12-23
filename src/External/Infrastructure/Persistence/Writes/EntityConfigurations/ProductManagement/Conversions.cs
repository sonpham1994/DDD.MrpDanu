using Domain.ProductManagement;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.Writes.EntityConfigurations.ProductManagement;

//https://learn.microsoft.com/en-us/ef/core/modeling/value-conversions?tabs=data-annotations&fbclid=IwAR2PKlDh2q6x2O6jcncKj8-RTiQOfsiAIRMaMmDaUHJ70quA-T7xsS-Tt-o
//https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/breaking-changes?fbclid=IwAR06Of9WHCQuh4ee7RvvP2h7-hwDBuZlqNXZhD3Rm89A_4B8VplUqfxR9Jc
internal sealed class ProductIdConverter : ValueConverter<ProductId, uint>
{
    public ProductIdConverter()
        : base(
            v => v.Value,
            v => new ProductId(v))
    {
    }
}

internal sealed class BoMIdConverter : ValueConverter<BoMId, uint>
{
    public BoMIdConverter()
        : base(
            v => v.Value,
            v => new BoMId(v))
    {
    }
}

internal sealed class BoMRevisionIdConverter : ValueConverter<BoMRevisionId, ushort>
{
    public BoMRevisionIdConverter()
        : base(
            v => v.Value,
            v => new BoMRevisionId(v))
    {
    }
}

internal sealed class BoMRevisionMaterialIdConverter : ValueConverter<BoMRevisionMaterialId, Guid>
{
    public BoMRevisionMaterialIdConverter()
        : base(
            v => v.Value,
            v => (BoMRevisionMaterialId)v)
    {
    }
}