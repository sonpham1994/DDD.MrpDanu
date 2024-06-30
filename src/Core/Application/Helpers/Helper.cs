using System.Diagnostics;

namespace Application.Helpers;

public static class Helper
{
    public static string? GetTraceId()
    {
        return Activity.Current?.Id;
    }
}