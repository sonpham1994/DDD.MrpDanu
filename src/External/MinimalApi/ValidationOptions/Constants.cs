namespace MinimalApi.ValidationOptions;

public static class Constants
{
    // create a string with char for not be interned. Because we just use this at once when we start program, hence we
    // don't need to allocate this value string in high-frequency heap. And when Gc 2 is perform collection process.
    // These value on high-frequency heap are never collected. By creating string as char[], these value will not live
    // in High-Frequency heap and then they are eligible for collection of GC. Please check TestGcForString project
    //Please check Benchmark/StringInternOrHighFrequencyHeap
    private static char[] _serverDatabaseKeyChars => ['S','e','r','v','e','r'];
    public static string ServerDatabaseKey => new(_serverDatabaseKeyChars);

    private static char[] _catalogDatabaseKeyChars => ['C', 'a', 't', 'a', 'l', 'o', 'g'];
    private static char[] _initialCatalogDatabaseKeyChars => ['I','n','i','t','i','a','l',' ', .._catalogDatabaseKeyChars];
    private static char[] _userIdDatabaseKeyChars => ['U', 's', 'e', 'r', ' ', 'I', 'd'];
    private static char[] _passwordDatabaseKeyChars => ['P','a','s','s','w','o','r','d'];
    public static string CatalogDatabaseKey => new(_catalogDatabaseKeyChars);
    public static string InitialCatalogDatabaseKey => new(_initialCatalogDatabaseKeyChars);
    public static string UserIdDatabaseKey => new(_userIdDatabaseKeyChars);
    public static string PasswordDatabaseKey => new(_passwordDatabaseKeyChars);
}