namespace TipsAndTricksLibrary.Redis.StackExhange
{
    using System;
    using System.Linq;
    using Converters;
    using StackExchange.Redis;

    public class CacheConnection : ICacheConnection
    {
        private readonly IDatabase _database;
        private readonly IKeysConverter _keysConverter;
        private readonly IValuesConverter _valuesConverter;

        public CacheConnection(IDatabase database, IKeysConverter keysConverter, IValuesConverter valuesConverter)
        {
            if(database == null)
                throw new ArgumentNullException(nameof(database));
            if (keysConverter == null)
                throw new ArgumentNullException(nameof(keysConverter));
            if (valuesConverter == null)
                throw new ArgumentNullException(nameof(valuesConverter));

            _database = database;
            _keysConverter = keysConverter;
            _valuesConverter = valuesConverter;
        }

        public void AddValue(object key, object value, TimeSpan? lifeTime = null)
        {
            _database.StringSet(_keysConverter.Convert(key), _valuesConverter.ConvertTo(value), lifeTime);
        }

        public void UpdateValue(object key, object value, TimeSpan? lifeTime = null)
        {
            _database.StringSet(_keysConverter.Convert(key), _valuesConverter.ConvertTo(value), lifeTime);
        }

        public TValue GetValue<TValue>(object key)
        {
            return _valuesConverter.ConvertFrom<TValue>(_database.StringGet(_keysConverter.Convert(key)));
        }

        public void DeleteByKeys<TKey>(params TKey[] keys)
        {
            _database.KeyDelete(keys.Select(x => _keysConverter.Convert(x)).ToArray());
        }
    }
}