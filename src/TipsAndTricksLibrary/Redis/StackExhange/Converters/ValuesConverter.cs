namespace TipsAndTricksLibrary.Redis.StackExhange.Converters
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using StackExchange.Redis;
    using TypeExtensions = Extensions.TypeExtensions;

    public class ValuesConverter : IValuesConverter
    {
        private readonly Dictionary<Type, Func<object, RedisValue>> _inConversions;
        private readonly Dictionary<Type, Func<RedisValue, object>> _outConversions;

        public ValuesConverter()
        {
            _inConversions = TypeExtensions.GetAllConversionsIn<RedisValue>();
            _outConversions = TypeExtensions.GetAllConversionsOut<RedisValue>();
        }

        public RedisValue ConvertTo<TValue>(TValue value)
        {
            Func<object, RedisValue> conversion;
            if (_inConversions.TryGetValue(typeof(TValue), out conversion))
                return conversion(value);
            return JsonConvert.SerializeObject(value);
        }

        public TValue ConvertFrom<TValue>(RedisValue value)
        {
            Func<RedisValue, object> conversion;
            if (_outConversions.TryGetValue(typeof(TValue), out conversion))
                return (TValue)conversion(value);
            return JsonConvert.DeserializeObject<TValue>(value);
        }
    }
}