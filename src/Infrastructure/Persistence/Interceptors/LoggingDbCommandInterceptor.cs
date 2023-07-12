using System.Data.Common;
using Infrastructure.Persistence.DbCommands;
using Infrastructure.Persistence.LoggingDefinitions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Interceptors;

internal sealed class LoggingDbCommandInterceptor : DbCommandInterceptor
{
    private readonly ILogger<LoggingDbCommandInterceptor> _logger;
    private readonly double _standardExecutedDbCommandTime;

    public LoggingDbCommandInterceptor(ILogger<LoggingDbCommandInterceptor> logger, IOptions<DatabaseSettings> databaseSettings)
    {
        _logger = logger;
        _standardExecutedDbCommandTime = databaseSettings.Value.StandardExecutedDbCommandTime;
    }

    public override ValueTask<DbDataReader> ReaderExecutedAsync(DbCommand command,
        CommandExecutedEventData eventData,
        DbDataReader result,
        CancellationToken cancellationToken = default)
    {
        CustomLoggingDbContext(command, eventData, nameof(ReaderExecutedAsync));
        
        return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
    }

    public override DbDataReader ReaderExecuted(DbCommand command, 
        CommandExecutedEventData eventData, 
        DbDataReader result)
    {
        //Lazy loading go through this method.
        CustomLoggingDbContext(command, eventData, nameof(ReaderExecuted));
        
        return base.ReaderExecuted(command, eventData, result);
    }

    private void CustomLoggingDbContext(DbCommand command, CommandExecutedEventData eventData, string methodName)
    {
        double durationInMilliSeconds = eventData.Duration.TotalMilliseconds;

        ParameterDbCommand[] parameters = new ParameterDbCommand[command.Parameters.Count];
        for (int i = 0; i < command.Parameters.Count; i++)
        {
            var parameter = command.Parameters[i];
            parameters[i] = new ParameterDbCommand(parameter.ParameterName, parameter.Value);
        }
        
        if (durationInMilliSeconds > _standardExecutedDbCommandTime)
        {
            _logger.StartLogLongDbCommand(methodName, durationInMilliSeconds, parameters, command.CommandText);
        }
        else
        {
            _logger.StartLogDbCommand(methodName, durationInMilliSeconds, parameters, command.CommandText);
        }
    }
}