namespace MinimalApi.Constants;

public static class MongoDbConstants
{
    // Please check ReadOnlySpanBenchmark for ReadOnlySpan<char>
    private static ReadOnlySpan<char> MongoDbChars => ['M', 'o', 'n', 'g', 'o', 'D', 'b'];
    public static string ConfigurationName => new([..MongoDbChars,..CommonConstants.SettingsChars]);
}