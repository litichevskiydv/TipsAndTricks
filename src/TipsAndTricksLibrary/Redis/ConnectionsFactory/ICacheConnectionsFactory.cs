namespace TipsAndTricksLibrary.Redis.ConnectionsFactory
{
    using System;

    public interface ICacheConnectionsFactory : IDisposable
    {
        ICacheConnection Create();
    }
}