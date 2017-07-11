namespace TipsAndTricksLibrary.Redis.StackExhange.Converters
{
    using StackExchange.Redis;

    public interface IKeysConverter
    {
        RedisKey Convert(object key);

        RedisKey Convert<TKey>(TKey key);
    }
}