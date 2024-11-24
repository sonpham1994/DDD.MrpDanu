namespace MinimalApi.Settings;

public sealed class SerilogSettings
{
    public IReadOnlyList<WriteTo> WriteTo { get; init; } = [];
}

public sealed class WriteTo
{
    public string Name { get; init; } = string.Empty;
    public Args Args { get; init; } = default!;

}

public sealed class Args
{
    public string ConnectionString { get; init; } = string.Empty;
}