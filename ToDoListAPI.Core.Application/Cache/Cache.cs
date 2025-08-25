using System.Collections.Concurrent;

namespace ToDoListAPI.Core.Application.Cache
{
    public class Cache<TKey, TValue>
    {
        private ConcurrentDictionary<TKey, CacheItem<TValue>> cache = new();

        public bool getValue(TKey key, out TValue value)
        {
            value = default;
            if (cache.TryGetValue(key, out var item))
            {
                if (item.Expiracion > DateTime.Now)
                {
                    value = item.Value;
                    return true;
                }
                else
                {
                    cache.TryRemove(key, out _);
                }
            }
            return false;
        }

        public void set(TKey key, TValue value, int ttlSeconds = 60)
        {
            var item = new CacheItem<TValue>
            {
                Value = value,
                Expiracion = DateTime.Now.AddSeconds(ttlSeconds)
            };
            cache.AddOrUpdate(key, item, (key, item) => item);
        }

        public void remove(TKey key)
        {
            cache.TryRemove(key, out _);
        }
    }

}
