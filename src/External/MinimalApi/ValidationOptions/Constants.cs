namespace MinimalApi.ValidationOptions;

public static class Constants
{
    // create a string with char for not be interned. Because we just use this at once when we start program, hence we
    // don't need to allocate this value string in high-frequency heap. And when Gc 2 is perform collection process.
    // These value on high-frequency heap are never collected. By creating string as char[], these value will not live
    // in High-Frequency heap and then they are eligible for collection of GC. Please check TestGcForString project
    //Please check Benchmark/StringInternOrHighFrequencyHeap
    // Please check ReadOnlySpanBenchmark for ReadOnlySpan<char>
    private static ReadOnlySpan<char> _serverDatabaseKeyChars => ['S','e','r','v','e','r'];
    public static string ServerDatabaseKey => new(_serverDatabaseKeyChars);
    private static ReadOnlySpan<char> _catalogDatabaseKeyChars => ['C', 'a', 't', 'a', 'l', 'o', 'g'];
    private static ReadOnlySpan<char> _initial => ['I', 'n', 'i', 't', 'i', 'a', 'l'];
    private static ReadOnlySpan<char> _userIdDatabaseKeyChars => ['U', 's', 'e', 'r', ' ', 'I', 'd'];
    private static ReadOnlySpan<char> _passwordDatabaseKeyChars => ['P','a','s','s','w','o','r','d'];
    
    public static string CatalogDatabaseKey => new(_catalogDatabaseKeyChars);
    public static string InitialCatalogDatabaseKey => new([.._initial, .._catalogDatabaseKeyChars]);
    public static string UserIdDatabaseKey => new(_userIdDatabaseKeyChars);
    public static string PasswordDatabaseKey => new(_passwordDatabaseKeyChars);
}