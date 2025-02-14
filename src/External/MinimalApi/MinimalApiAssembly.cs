using System.Reflection;

namespace MinimalApi;

internal sealed class MinimalApiAssembly
{
    public static Assembly Instance => typeof(MinimalApiAssembly).Assembly;
}
