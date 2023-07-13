using System.Reflection;

namespace Domain;

public class DomainAssembly
{
    public static readonly Assembly Instance = typeof(DomainAssembly).Assembly;
}