namespace TipsAndTricksLibrary.Tests.Logging
{
    using Microsoft.Extensions.Logging;
    using NLog.Extensions.Logging;
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
        public void ShouldWriteInfoMessage()
        {
            _logger.LogInformation("Information was decoded successfully");
        }
    }
}