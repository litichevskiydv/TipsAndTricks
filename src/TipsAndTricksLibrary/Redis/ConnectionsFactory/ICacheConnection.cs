namespace TipsAndTricksLibrary.Redis.ConnectionsFactory
{
    using System;

    public interface ICacheConnection
    {
        void AddValue<TKey, TValue>(TKey key, TValue value, TimeSpan? lifeTime = null);

        void UpdateValue<TKey, TValue>(TKey key, TValue value, TimeSpan? lifeTime = null);

        void DeleteByKeys<TKey>(params TKey[] keys);
    }
}