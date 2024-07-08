namespace Api.LoggingDefinitions;

public static class LoggingTypes
{
    // create a string with char for not be interned. Because we just use this at once when we start program, hence we
    // don't need to allocate this value string in high-frequency heap
    // Please check Benchmark/StringInternOrHighFrequencyHeap
    private static char[] ValidationOptionChars => ['V','a','l','i','d','a','t','i','o','n','O','p','t','i','o','n'];
    public static string ValidationOption => new(ValidationOptionChars); // Generation 0
    //public static string ValidationOption => nameof(ValidationOption); // Generation 2147483647
    
    public const string GcEventListener = nameof(GcEventListener);
}