using System.Reflection;

namespace Infrastructure;

public class InfrastructureAssembly
{
    public static readonly Assembly Instance = typeof(InfrastructureAssembly).Assembly;
}
