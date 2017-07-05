namespace TipsAndTricksLibrary.Redis.ConnectionsFactory
{
    using System;
    using Converters;
    using Microsoft.Extensions.Options;
    using StackExchange.Redis;

    public class CacheConnectionsFactory : ICacheConnectionsFactory
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;

        public CacheConnectionsFactory(IOptions<CacheConnectionsFactoryOptions> options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            if (string.IsNullOrWhiteSpace(options.Value.Redis))
                throw new ArgumentNullException(nameof(CacheConnectionsFactoryOptions.Redis));

            var configuration = ConfigurationOptions.Parse(options.Value.Redis);
            _connectionMultiplexer = ConnectionMultiplexer.Connect(configuration);
            _connectionMultiplexer.IncludeDetailInExceptions = true;
        }

        public ICacheConnection Create()
        {
            return new CacheConnection(_connectionMultiplexer.GetDatabase(), new KeysConverter(), new ValuesConverter());
        }
    }
}