namespace TipsAndTricksLibrary.Redis.StackExhange
{
    using System;
    using System.Collections.Generic;
    using Converters;
    using Microsoft.Extensions.Options;
    using StackExchange.Redis;

    public class CacheConnectionsFactory : ICacheConnectionsFactory
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;

        protected bool Disposed;

        public CacheConnectionsFactory(IOptions<CacheConnectionsFactoryOptions> options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            if (string.IsNullOrWhiteSpace(options.Value.Redis))
                throw new ArgumentNullException(nameof(CacheConnectionsFactoryOptions.Redis));

            var configuration = ConfigurationOptions.Parse(options.Value.Redis);
            configuration.CommandMap = CommandMap.Create(new HashSet<string> {"SUBSCRIBE"}, false);
            configuration.SocketManager = new SocketManager("Custom", true);

            _connectionMultiplexer = ConnectionMultiplexer.Connect(configuration);
            _connectionMultiplexer.IncludeDetailInExceptions = true;
        }

        private void ValidateStatus()
        {
            if (Disposed)
                throw new ObjectDisposedException(nameof(ConnectionMultiplexer));
        }

        public ICacheConnection Create()
        {
            ValidateStatus();
            return new CacheConnection(_connectionMultiplexer.GetDatabase(), new KeysConverter(), new ValuesConverter());
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            if (disposing)
                _connectionMultiplexer?.Dispose();
            Disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~CacheConnectionsFactory()
        {
            Dispose(false);
        }
    }
}