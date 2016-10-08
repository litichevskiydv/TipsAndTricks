namespace TechnologyUsageTests.Logging
{
    using System;
    using Microsoft.Extensions.Logging;
    using NLog.Extensions.Logging;
    using TipsAndTricksLibrary.Logging;
    using Xunit;

    public class NlogUsageTests
    {
        private readonly ILogger _logger;

        public NlogUsageTests()
        {
            var loggerFactory = new LoggerFactory()
                .AddNLog();
            _logger = loggerFactory.CreateLogger<NlogUsageTests>();
        }

        [Fact]
        public void ShouldLogInfoMessage()
        {
            _logger.LogInformation("Information was decoded successfully");
        }

        [Fact]
        public void ShouldLogErrorMessage()
        {
            _logger.LogError("Something went wrong");
        }

        [Fact]
        public void ShouldLogException()
        {
            _logger.LogError(new EventId(1), new InvalidOperationException("Wrong operation sign"), "Something went wrong");
        }

        [Fact]
        public void ShouldLogExceptionWithoutEventId()
        {
            _logger.LogError(new InvalidOperationException("Wrong operation sign"), "Something went wrong");
        }
    }
}