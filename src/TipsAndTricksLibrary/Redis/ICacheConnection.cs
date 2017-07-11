namespace TipsAndTricksLibrary.Redis
{
    using System;

    public interface ICacheConnection
    {
        void AddValue<TKey, TValue>(TKey key, TValue value, TimeSpan? lifeTime = null);

        void UpdateValue<TKey, TValue>(TKey key, TValue value, TimeSpan? lifeTime = null);

        TValue GetValue<TValue>(object key);

        void DeleteByKeys<TKey>(params TKey[] keys);
    }
}