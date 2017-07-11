namespace TipsAndTricksLibrary.Redis.StackExhange.Converters
{
    using StackExchange.Redis;

    public interface IValuesConverter
    {
        RedisValue ConvertTo(object value);

        RedisValue ConvertTo<TValue>(TValue value);

        TValue ConvertFrom<TValue>(RedisValue value);
    }
}