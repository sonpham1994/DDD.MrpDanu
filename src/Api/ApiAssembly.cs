using System.Reflection;

namespace Api;

internal sealed class ApiAssembly
{
    public static Assembly Instance => typeof(ApiAssembly).Assembly;
}
