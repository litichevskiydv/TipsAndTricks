namespace TipsAndTricksLibrary.Redis.ConnectionsFactory
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

        public void AddValue<TKey, TValue>(TKey key, TValue value, TimeSpan? lifeTime = null)
        {
            _database.StringSet(_keysConverter.Convert(key), _valuesConverter.Convert(value), lifeTime);
        }

        public void UpdateValue<TKey, TValue>(TKey key, TValue value, TimeSpan? lifeTime = null)
        {
            _database.StringSet(_keysConverter.Convert(key), _valuesConverter.Convert(value), lifeTime);
        }

        public void DeleteByKeys<TKey>(params TKey[] keys)
        {
            _database.KeyDelete(keys.Select(x => _keysConverter.Convert(x)).ToArray());
        }
    }
}