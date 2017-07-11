namespace TipsAndTricksLibrary.Redis.StackExhange.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Extensions;
    using Microsoft.CSharp.RuntimeBinder;
    using StackExchange.Redis;

    public class KeysConverter : IKeysConverter
    {
        private readonly Dictionary<Type, Func<object, RedisKey>> _availableConversions;

        public KeysConverter()
        {
            _availableConversions = TypeExtensions.GetAllConversionsIn<RedisKey>();
        }

        public RedisKey Convert(object key)
        {
            Func<object, RedisKey> conversion;
            if (_availableConversions.TryGetValue(key.GetType(), out conversion))
                return conversion(key);

            try
            {
                return ((dynamic)key).ToString(CultureInfo.InvariantCulture);
            }
            catch (RuntimeBinderException)
            {
                return key.ToString();
            }
        }

        public RedisKey Convert<TKey>(TKey key)
        {
            return Convert((object)key);
        }
    }
}