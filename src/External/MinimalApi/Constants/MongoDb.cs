namespace MinimalApi.Constants;

public static class MongoDb
{
    private static char[] MongoDbChars => ['M', 'o', 'n', 'g', 'o', 'D', 'b'];
    public static string ConfigurationName => new([..MongoDbChars,..CommonConstants.SettingsChars]);
}