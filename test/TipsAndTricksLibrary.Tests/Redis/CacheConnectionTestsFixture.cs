﻿namespace TipsAndTricksLibrary.Tests.Redis
{
    using System;
    using JetBrains.Annotations;
    using Microsoft.Extensions.Options;
    using TipsAndTricksLibrary.Redis.ConnectionsFactory;

    [UsedImplicitly]
    public class CacheConnectionTestsFixture : IDisposable
    {
        public CacheConnectionsFactory ConnectionsFactory { get; }
        public Guid Key { get; }

        public CacheConnectionTestsFixture()
        {
            ConnectionsFactory = new CacheConnectionsFactory(
                Options.Create(new CacheConnectionsFactoryOptions
                               {
                                   Redis = "localhost:6379,defaultDatabase=1,connectTimeout=10000,syncTimeout=10000,connectRetry=30,keepAlive=5,abortConnect=false"
                }));
            Key = Guid.NewGuid();
        }

        public void Dispose()
        {
            ConnectionsFactory.Create().DeleteByKeys(Key);
            ConnectionsFactory.Dispose();
        }
    }
}