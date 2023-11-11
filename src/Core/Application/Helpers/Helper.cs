using System.Diagnostics;

namespace Application.Helpers;

public class Helper
{
    public static string GetTraceId()
    {
        return Activity.Current?.Id;
    }
}