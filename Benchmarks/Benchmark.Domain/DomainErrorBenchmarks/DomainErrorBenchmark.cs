using BenchmarkDotNet.Attributes;

/*
 * https://www.youtube.com/watch?v=bnVfrd3lRv8&t=870s&ab_channel=NickChapsas
 * https://www.youtube.com/watch?v=a26zu-pyEyg&t=28s&ab_channel=NickChapsas
 */
namespace Benchmark.Domain.DomainErrorBenchmarks;

[MemoryDiagnoser()]
public class DomainErrorBenchmark
{
    public const string ConstStringMessageTemplate = "Material id '{0}' is not found";
    public static string StaticStringMessageTemplate = "Material id '{0}' is not found";
    public const string CodeTemplate = "Material.NotFoundId";
    public static string StaticCodeTemplate = "Material.NotFoundId";
    public static string StringWithoutMessageTemplate(Guid id) => $"Material id '{id}' is not found";

    private static readonly Func<Guid, DomainError> _cachedMaterialIdNotFound = (id) =>
        new DomainError(CodeTemplate, $"Material id '{id}' is not found");


    public static DomainError MaterialIdNotFoundStaticMessageAndCodeTemplate(Guid id) => new(CodeTemplate, string.Format(StaticStringMessageTemplate, id));
    public static DomainError MaterialIdNotFoundStaticMessageTemplate(Guid id) => new("Material.NotFoundId", string.Format(StaticStringMessageTemplate, id));
    public static DomainError MaterialIdNotFoundConstMessageAndCodeTemplate(Guid id) => new(CodeTemplate, string.Format(ConstStringMessageTemplate, id));
    public static DomainError MaterialIdNotFoundConstMessageTemplate(Guid id) => new("Material.NotFoundId", string.Format(ConstStringMessageTemplate, id));
    public static DomainError MaterialIdNotFound(Guid id) => new("Material.NotFoundId", $"Material id '{id}' is not found");
    public static DomainError MaterialIdNotFoundMessageAndConstCodeTemplate(Guid id) => new(CodeTemplate, $"Material id '{id}' is not found");

    public static DomainError CachedMaterialIdNotFound(Guid id)
    {
        return _cachedMaterialIdNotFound(id);
    }

    public static Guid Id = Guid.NewGuid();
    [Params(10, 100, 1000, 10000)]
    public int Length { get; set; }

    [Benchmark]
    public void CreateDomainErrorWithoutMessageAndCodeTemplate()
    {
        for (int i = 0; i < Length; i++)
        {
            MaterialIdNotFound(Id);
        }

    }

    [Benchmark]
    public void CreateDomainErrorWithStaticMessageTemplate()
    {
        for (int i = 0; i < Length; i++)
        {
            MaterialIdNotFoundStaticMessageTemplate(Id);
        }
    }

    [Benchmark]
    public void CreateDomainErrorWithStaticMessageAndCodeTemplate()
    {
        for (int i = 0; i < Length; i++)
        {
            MaterialIdNotFoundStaticMessageAndCodeTemplate(Id);
        }
    }

    [Benchmark]
    public void CreateDomainErrorWithConstMessageTemplate()
    {
        for (int i = 0; i < Length; i++)
        {
            MaterialIdNotFoundConstMessageTemplate(Id);
        }
    }

    [Benchmark]
    public void CreateDomainErrorWithConstMessageAndCodeTemplate()
    {
        for (int i = 0; i < Length; i++)
        {
            MaterialIdNotFoundConstMessageAndCodeTemplate(Id);
        }
    }

    [Benchmark]
    public void CreateCacheDomainError()
    {
        for (int i = 0; i < Length; i++)
        {
            CachedMaterialIdNotFound(Id);
        }
    }

    [Benchmark]
    public void CreateDomainErrorWithMessageAndConstCodeTemplate()
    {
        for (int i = 0; i < Length; i++)
        {
            MaterialIdNotFoundMessageAndConstCodeTemplate(Id);
        }
    }

    [Benchmark]
    public void CreateStringWithoutTemplate()
    {
        for (int i = 0; i < Length; i++)
        {
            StringWithoutMessageTemplate(Id);
        }
    }

    [Benchmark]
    public void CreateConstStringWithTemplate()
    {
        for (int i = 0; i < Length; i++)
        {
            string.Format(ConstStringMessageTemplate, Id);
        }
    }

    [Benchmark]
    public void CreateStaticStringWithTemplate()
    {
        for (int i = 0; i < Length; i++)
        {
            string.Format(StaticStringMessageTemplate, Id);
        }
    }
}