namespace Domain.ProductManagement;

public record struct BoMId(uint Value);

public record struct ProductId(uint Value);

public record struct BoMRevisionId(ushort Value);

public record struct BoMRevisionMaterialId(Guid Value);

public record struct MaterialId(Guid Value);

public record struct SupplierId(Guid Value);