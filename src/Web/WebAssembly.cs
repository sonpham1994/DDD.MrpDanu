using System.Reflection;

namespace Web;

internal sealed class WebAssembly
{
    public static Assembly Instance => typeof(WebAssembly).Assembly;
}
