using Microsoft.Extensions.Logging;

namespace Benchmark.Application.LoggingMessage;

public static class LoggingMessageDefinition
{
    private static readonly Action<ILogger, string, int, Exception?> _logMessageInfoDefinition =
        LoggerMessage.Define<string, int>(LogLevel.Information, 0, "My name is {Name} and {Age} years old");

    private static readonly Action<ILogger, string, int, Exception?> _logMessageInfoDefinitionWithStruct =
        LoggerMessage.Define<string, int>(LogLevel.Information, 0, "My name is {Name} and {Age} years old");
    
    private static readonly Action<ILogger, string, int, Exception?> _logMessageInfoDefinitionWithObjectStruct =
        LoggerMessage.Define<string, int>(LogLevel.Information, 0, "My name is {Name} and {Age} years old");
    
    public static void LogMessageInfoDefinition(this ILogger logger, string name, int age)
    {
        _logMessageInfoDefinition(logger, name, age, null);
    }
    
    public static void LogMessageInfoDefinitionWithStruct(this ILogger logger, LoggingMessageBenchmark.Person person)
    {
        _logMessageInfoDefinitionWithStruct(logger, person.Name, person.Age, null);
    }
    
    public static void LogMessageInfoDefinitionWithObjectStruct(this ILogger logger, object person)
    {
        if (person is LoggingMessageBenchmark.Person t) 
            _logMessageInfoDefinitionWithObjectStruct(logger, t.Name, t.Age, null);
    }
}