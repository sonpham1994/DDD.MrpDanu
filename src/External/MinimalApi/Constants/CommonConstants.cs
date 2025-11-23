namespace MinimalApi.Constants;

public static class CommonConstants
{
    // create a string with char for not be interned. Because we just use this at once when we start program, hence we
    // don't need to allocate this value string in high-frequency heap. And when Gc 2 is perform collection process.
    // These value on high-frequency heap are never collected. By creating string as char[], these value will not live
    // in High-Frequency heap and then they are eligible for collection of GC. Please check TestGcForString project
    //Please check Benchmark/StringInternOrHighFrequencyHeap
    // Please check ReadOnlySpanBenchmark for ReadOnlySpan<char>
    public static ReadOnlySpan<char> ConnectionString => ['C', 'o', 'n', 'n', 'e', 'c', 't', 'i', 'o', 'n', 'S', 't', 'r', 'i', 'n', 'g'];
    public static ReadOnlySpan<char> IsEmpty => ['I', 's', ' ', 'e', 'm',  'p', 't', 'y'];
    public static ReadOnlySpan<char> ConnectionStringEmpty => new([..ConnectionString, ' ', ..IsEmpty]);
    public static ReadOnlySpan<char> SettingsChars => ['S', 'e', 't', 't', 'i', 'n', 'g', 's'];
    public static ReadOnlySpan<char> Name => ['N', 'a', 'm', 'e'];
}