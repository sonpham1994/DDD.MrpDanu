using Domain.SharedKernel;
using Domain.SharedKernel.ValueObjects;
using FluentAssertions;

namespace Domain.Tests.MaterialManagement.MaterialAggregate;

public class MaterialSupplierIdentityTests
{
    [Fact]
    public void Cannot_create_MaterialSupplierIdentity_if_material_id_is_empty()
    {
        var materialId = (MaterialId)Guid.Empty;
        var result = MaterialSupplierIdentity.Create(materialId, (SupplierId)Guid.NewGuid());

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.MaterialSupplierIdentity.InvalidMaterialId(materialId));
    }

    [Fact]
    public void Cannot_create_MaterialSupplierIdentity_if_supplier_id_is_empty()
    {
        var supplierId = (SupplierId)Guid.Empty;
        var result = MaterialSupplierIdentity.Create((MaterialId)Guid.NewGuid(), supplierId);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.MaterialSupplierIdentity.InvalidSupplierId(supplierId));
    }

    [Fact]
    public void Create_MaterialSupplierIdentity_successfully()
    {
        var result = MaterialSupplierIdentity.Create((MaterialId)Guid.NewGuid(), (SupplierId)Guid.NewGuid());

        result.IsSuccess.Should().BeTrue();
    }
}