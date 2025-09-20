using Infrastructure.Persistence;
using Microsoft.Extensions.Options;

namespace MinimalApi;

public class TestingOptions
{
    public TestingOptions(IOptionsMonitor<SqlDatabaseSettings> databaseSettings)
    {
        //useful for case updating configuration.
        databaseSettings.OnChange((newSetting, a) =>
        {
            var b = newSetting;
        });
    }
}