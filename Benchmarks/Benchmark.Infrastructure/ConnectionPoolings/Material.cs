namespace Benchmark.Infrastructure.ConnectionPoolings;

public class Material
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string ColorCode { get; set; }
    public string Width{get; set; }
    public string Weight { get; set; }
    public string Unit { get; set; }
    public string Varian { get; set; }
    public string CodeUnique { get; set; }
    public byte MaterialTypeId { get; set; }
    public byte RegionalMarketId { get; set; }
}