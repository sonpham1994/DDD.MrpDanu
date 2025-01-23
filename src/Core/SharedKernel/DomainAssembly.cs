using System.Reflection;

namespace SharedKernel;

public sealed class SharedKernelAssembly
{
    public static Assembly Instance => typeof(SharedKernelAssembly).Assembly;
}