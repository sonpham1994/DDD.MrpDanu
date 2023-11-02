using System.Reflection;

namespace Api;

internal sealed class WebAssembly
{
    public static Assembly Instance => typeof(WebAssembly).Assembly;
}
