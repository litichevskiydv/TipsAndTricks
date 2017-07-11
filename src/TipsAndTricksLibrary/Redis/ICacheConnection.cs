namespace TipsAndTricksLibrary.Redis
{
    using System;

    public interface ICacheConnection
    {
        void AddValue(object key, object value, TimeSpan? lifeTime = null);

        void UpdateValue(object key, object value, TimeSpan? lifeTime = null);

        TValue GetValue<TValue>(object key);

        void DeleteByKeys<TKey>(params TKey[] keys);
    }
}