using FluentResults;
using Microsoft.Extensions.Logging;

namespace VDesk.Utils;

public static class LoggerExtensions
{
    public static void LogError(this ILogger logger, IList<IError> errors, params object?[] args)
    {
        foreach (var error in errors)
        {
            logger.Log(LogLevel.Error, error.Message, args);
        }

    }
}