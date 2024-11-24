namespace MinimalApi.Constants;

public static class Serilog
{
    // create a string with char for not be interned. Because we just use this at once when we start program, hence we
    // don't need to allocate this value string in high-frequency heap. And when Gc 2 is perform collection process.
    // These value on high-frequency heap are never collected. By creating string as char[], these value will not live
    // in High-Frequency heap and then they are eligible for collection of GC. Please check TestGcForString project
    //Please check Benchmark/StringInternOrHighFrequencyHeap
    //string: SerilogSettings
    private static char[] ConfigurationNameChars => ['S', 'e', 'r', 'i', 'l', 'o', 'g'];

    private static char[] ApplicationInsightsChars =>
    [
        'A', 'p', 'p', 'l', 'i', 'c', 'a', 't', 'i', 'o', 'n', 'I', 'n', 's', 'i', 'g', 'h', 't', 's'
    ];
    public static string ConfigurationName => new(ConfigurationNameChars);
    public static string ConfigurationNameSettings => new([..ConfigurationNameChars, ..CommonConstants.SettingsChars]);
    public static string ApplicationInsightsName => new(ApplicationInsightsChars);
}
