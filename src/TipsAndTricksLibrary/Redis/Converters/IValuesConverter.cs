namespace TipsAndTricksLibrary.Redis.Converters
{
    using StackExchange.Redis;

    public interface IValuesConverter
    {
        RedisValue Convert<TValue>(TValue value);
    }
}