namespace Infrastructure.Persistence;

//https://www.youtube.com/watch?v=wxYt0motww0
//https://www.youtube.com/watch?v=qRruEdjNVNE
//https://www.milanjovanovic.tech/blog/how-to-use-the-options-pattern-in-asp-net-core-7
//https://www.youtube.com/watch?v=J0EVd5HbtUY&ab_channel=NickChapsas
/*
 * Using Options pattern to represent strongly typed configuration, we have three approaches for this Options pattern
 * IOptions: when modify data in IOptions, it will change when you restart your application (As a Singleton)
 * IOptionsMonitor: this is effectively transient, within request you use currentValue, but you change sth and the rest
 *  of the operations will use the changed value
 * IOptionsSnapshot: this is actually scoped, if you start request and you change sth, but within the same request, you
 *  still use the old value, when starting another request, it will use the new value.
 *
 * We can use IOptions for validating configuration
 */
internal sealed class DatabaseSettings
{
    public string ConnectionString { get; init; } = string.Empty;
    public byte MaxRetryCount { get; init; } = 0;
    public int MaxRetryDelay { get; init; } = 0; //seconds
    public double StandardExecutedDbCommandTime { get; init; } = 0; //miliseconds
}