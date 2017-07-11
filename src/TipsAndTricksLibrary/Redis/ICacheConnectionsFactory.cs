namespace TipsAndTricksLibrary.Redis
{
    using System;

    public interface ICacheConnectionsFactory : IDisposable
    {
        ICacheConnection Create();
    }
}