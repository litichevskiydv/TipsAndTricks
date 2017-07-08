namespace TipsAndTricksLibrary.Redis.Converters
{
    using StackExchange.Redis;

    public interface IValuesConverter
    {
        RedisValue ConvertTo<TValue>(TValue value);

        TValue ConvertFrom<TValue>(RedisValue value);
    }
}