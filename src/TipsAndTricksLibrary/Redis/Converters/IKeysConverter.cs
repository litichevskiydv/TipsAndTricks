namespace TipsAndTricksLibrary.Redis.Converters
{
    using StackExchange.Redis;

    public interface IKeysConverter
    {
        RedisKey Convert<TKey>(TKey key);
    }
}