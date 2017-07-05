namespace TipsAndTricksLibrary.Redis.ConnectionsFactory
{
    public interface ICacheConnectionsFactory
    {
        ICacheConnection Create();
    }
}