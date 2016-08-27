namespace TipsAndTricksLibrary.Logging
{
    using System;
    using Microsoft.Extensions.Logging;

    public static class LoggerCustomExtensions
    {
        public static void LogError(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.LogError(0, exception, message, args);
        }
    }
}