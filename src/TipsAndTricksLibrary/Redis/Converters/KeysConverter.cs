namespace TipsAndTricksLibrary.Redis.Converters
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

        public RedisKey Convert<TKey>(TKey key)
        {
            Func<object, RedisKey> conversion;
            if(_availableConversions.TryGetValue(typeof(TKey), out conversion))
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
    }
}