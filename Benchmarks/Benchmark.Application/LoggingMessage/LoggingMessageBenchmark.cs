using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging;

namespace Benchmark.Application.LoggingMessage;

[MemoryDiagnoser()]
public class LoggingMessageBenchmark
{
    public readonly struct Person
    {
        public string Name { get; }
        public int Age { get; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
    private readonly ILoggerFactory _loggerInfoFactory = LoggerFactory.Create(builder =>
    {
        builder.AddConsole().SetMinimumLevel(LogLevel.Information);
    });
    private readonly ILoggerFactory _loggerWarningFactory = LoggerFactory.Create(builder =>
    {
        builder.AddConsole().SetMinimumLevel(LogLevel.Warning);
    });
    private readonly ILogger<LoggingMessageBenchmark> _loggerInfo;
    private readonly ILogger<LoggingMessageBenchmark> _loggerWarning;

    public LoggingMessageBenchmark()
    {
        _loggerInfo = new Logger<LoggingMessageBenchmark>(_loggerInfoFactory);
        
        _loggerWarning = new Logger<LoggingMessageBenchmark>(_loggerWarningFactory);
    }

    [Benchmark]
    public void LoggingInfoMessage()
    {
        int age = 30;
        string name = "Son";
        _loggerInfo.LogInformation("My name is {Name} and {Age} years old", name, age);
    }
    
    [Benchmark]
    public void LoggingInfoMessageDefinition()
    {
        int age = 30;
        string name = "Son";
        _loggerInfo.LogMessageInfoDefinition(name, age);
    }
    
    [Benchmark]
    public void LoggingWarningMessage()
    {
        int age = 30;
        string name = "Son";
        _loggerWarning.LogInformation("My name is {Name} and {Age} years old", name, age);
    }
    
    [Benchmark]
    public void LoggingWarningMessageDefinition()
    {
        int age = 30;
        string name = "Son";
        _loggerWarning.LogMessageInfoDefinition(name, age);
    }
    
    [Benchmark]
    public void LoggingInfoMessageDefinitionWithStruct()
    {
        var person = new Person("Son", 30);
        _loggerInfo.LogMessageInfoDefinitionWithStruct(person);
    }
    
    [Benchmark]
    public void LoggingInfoMessageDefinitionWithObjectStruct()
    {
        var person = new Person("Son", 30);
        _loggerInfo.LogMessageInfoDefinitionWithObjectStruct(person);
    }
    
    [Benchmark]
    public void LoggingWarningMessageDefinitionWithStruct()
    {
        var person = new Person("Son", 30);
        _loggerWarning.LogMessageInfoDefinitionWithStruct(person);
    }
    
    [Benchmark]
    public void LoggingWarningMessageDefinitionWithObjectStruct()
    {
        var person = new Person("Son", 30);
        _loggerWarning.LogMessageInfoDefinitionWithObjectStruct(person);
    }
}