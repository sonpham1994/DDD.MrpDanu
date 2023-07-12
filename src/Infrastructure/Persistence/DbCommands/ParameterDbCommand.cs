namespace Infrastructure.Persistence.DbCommands;

internal readonly struct ParameterDbCommand
{
    public string ParameterName { get; }
    public object? Value { get; }

    public ParameterDbCommand(string parameterName, object? value)
    {
        ParameterName = parameterName;
        Value = value;
    }
}