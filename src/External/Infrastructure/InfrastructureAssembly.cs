using System.Reflection;

namespace Infrastructure;

internal sealed class InfrastructureAssembly
{
    public static Assembly Instance => typeof(InfrastructureAssembly).Assembly;
}
