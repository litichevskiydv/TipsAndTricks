namespace TipsAndTricksLibrary.Redis.Converters
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using StackExchange.Redis;
    using TypeExtensions = Extensions.TypeExtensions;

    public class ValuesConverter : IValuesConverter
    {
        private readonly Dictionary<Type, Func<object, RedisValue>> _availableConversions;

        public ValuesConverter()
        {
            _availableConversions = TypeExtensions.GetAllConversionsIn<RedisValue>();
        }

        public RedisValue Convert<TValue>(TValue value)
        {
            Func<object, RedisValue> converter;
            if (_availableConversions.TryGetValue(typeof(TValue), out converter))
                return converter(value);
            return JsonConvert.SerializeObject(value);
        }
    }
}