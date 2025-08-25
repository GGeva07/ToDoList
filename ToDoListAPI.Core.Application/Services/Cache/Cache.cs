using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using ToDoListAPI.Core.Application.Services.Cache;

namespace ToDoListAPI.Core.Application.Services.Cache
{
    public class Cache<TKey, TValue>
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<Cache<TKey, TValue>>? _logger;

        public Cache(IMemoryCache memoryCache, ILogger<Cache<TKey, TValue>>? logger = null)
        {
            _cache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _logger = logger;
        }

        public bool getValue(TKey key, out TValue value)
        {
            value = default;

            if (_cache.TryGetValue(key, out string? valorS) && valorS != null)
            {
                try
                {
                    value = JsonSerializer.Deserialize<TValue>(valorS)!;
                    return true;
                }
                catch (JsonException ex)
                {
                    _logger?.LogWarning(ex, "Hubo un error tratando de dezerializar: {key}", key);
                    _cache.Remove(key);
                }
            }

            return false;
        }

        public void set(TKey key, TValue value, int ttlSeconds = 60)
        {
            var valorS = JsonSerializer.Serialize(value);

            var opciones = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(ttlSeconds),
                Priority = CacheItemPriority.Normal
            };

            _cache.Set(key, valorS, opciones);
        }

        public void remove(TKey key)
        {
            _cache.Remove(key);
        }
    }
}

public static class CacheServiceExtensions
{
    public static IServiceCollection AddCustomCache(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddScoped(typeof(Cache<,>));
        return services;
    }
}