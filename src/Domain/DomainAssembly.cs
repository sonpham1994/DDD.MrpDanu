using System.Reflection;

namespace Domain;

public sealed class DomainAssembly
{
    public static Assembly Instance => typeof(DomainAssembly).Assembly;
}