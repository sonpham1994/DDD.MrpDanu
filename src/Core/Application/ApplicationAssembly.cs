using System.Reflection;

namespace Application;

public sealed class ApplicationAssembly
{
    public static Assembly Instance => typeof(ApplicationAssembly).Assembly;
}