namespace MinimalApi.Constants;

public static class SqlDatabaseConstants
{
    // create a string with char for not be interned. Because we just use this at once when we start program, hence we
    // don't need to allocate this value string in high-frequency heap. And when Gc 2 is perform collection process.
    // These value on high-frequency heap are never collected. By creating string as char[], these value will not live
    // in High-Frequency heap and then they are eligible for collection of GC. Please check TestGcForString project
    //Please check Benchmark/StringInternOrHighFrequencyHeap
    // Please check ReadOnlySpanBenchmark for ReadOnlySpan<char>
    private static ReadOnlySpan<char> _initialChars => ['I','n','i','t','i','a','l'];
    private static ReadOnlySpan<char> _sqlDatabaseChars => ['S','q','l','D','a','t','a','b','a','s','e'];
    private static ReadOnlySpan<char> _catalogChars => ['C', 'a', 't', 'a', 'l', 'o', 'g'];
    private static ReadOnlySpan<char> _userIdChars => ['U', 's', 'e', 'r', ' ', 'I', 'd'];
    private static ReadOnlySpan<char> _passwordChars => ['P','a','s','s','w','o','r','d'];
    private static ReadOnlySpan<char> _serverChars => ['S','e','r','v','e','r'];
    
    public static string DatabaseStringKey => new(_sqlDatabaseChars);
    public static string ServerDatabaseKey => new([.._sqlDatabaseChars, .._serverChars]);
    public static string CatalogDatabaseKey => new(_catalogChars);
    public static string InitialCatalogDatabaseKey => new([.._initialChars, .._catalogChars]);
    public static string UserIdKey => new(_userIdChars);
    public static string PasswordKey => new(_passwordChars);
    
    public static string ConfigurationName => new([.._sqlDatabaseChars, ..CommonConstants.SettingsChars]);
    public static string ConnectionStringEmpty => new([.._sqlDatabaseChars, ' ', ..CommonConstants.SettingsChars, ' ', ..CommonConstants.ConnectionStringEmpty]);
    public static string ServerNameEmpty => new([.._sqlDatabaseChars, .._serverChars, ..CommonConstants.Name, ..CommonConstants.IsEmpty]);
    public static string DatabaseNameEmpty => new([.._sqlDatabaseChars,  ..CommonConstants.Name, ..CommonConstants.IsEmpty]);
    public static string UserIdEmpty => new([.._userIdChars, ..CommonConstants.IsEmpty]);
    public static string PasswordEmpty => new([.._passwordChars, ..CommonConstants.IsEmpty]);
}