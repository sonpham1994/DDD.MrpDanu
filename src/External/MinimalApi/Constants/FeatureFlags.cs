namespace MinimalApi.Constants;

public static class FeatureFlags
{
    // create a string with char for not be interned. Because we just use this at once when we start program, hence we
    // don't need to allocate this value string in high-frequency heap. And when Gc 2 is perform collection process.
    // These value on high-frequency heap are never collected. By creating string as char[], these value will not live
    // in High-Frequency heap and then they are eligible for collection of GC. Please check TestGcForString project
    //Please check Benchmark/StringInternOrHighFrequencyHeap
    // Please check ReadOnlySpanBenchmark for ReadOnlySpan<char>
    private static ReadOnlySpan<char> ConfigurationNameChars => ['F', 'e', 'a', 't', 'u', 'r', 'e', 'F', 'l', 'a', 'g', 's'];
    public static string ConfigurationName => new(ConfigurationNameChars);
    
    public const string GcEventListener = "GcEventListener";
}