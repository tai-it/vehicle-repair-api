namespace VehicleRepairs.Shared.Caching
{
    using Microsoft.Extensions.Caching.Memory;
    using System;
    using System.Threading.Tasks;

    public partial class CacheManager : ICacheManager
    {
        protected static IMemoryCache _cache;

        public CacheManager()
        {
        }

        public CacheManager(IMemoryCache cache) : this()
        {
            _cache = cache;
        }

        protected static IMemoryCache Cache
        {
            get
            {
                return _cache;
            }
            private set
            {
                _cache = value;
            }
        }

        public void Remove(params string[] keys)
        {
            if (keys == null)
                return;
            foreach (var key in keys)
            {
                Cache.Remove(key);
            }
        }

        public T GetAndSet<T>(string key, int cacheSeconds, Func<T> func)
        {
            // Set cache options.
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                // Keep in cache for this time, reset time if accessed.
                .SetSlidingExpiration(TimeSpan.FromSeconds(cacheSeconds));

            if (!_cache.TryGetValue<T>(key, out T t))
            {
                t = func();

                // Save data in cache.
                _cache.Set(key, t, cacheEntryOptions);
            }
            return t;
        }

        public async Task<T> GetAndSetAsync<T>(object key, int cacheSeconds, Func<Task<T>> func)
        {
            // Set cache options.
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                // Keep in cache for this time, reset time if accessed.
                .SetSlidingExpiration(TimeSpan.FromSeconds(cacheSeconds));

            if (!_cache.TryGetValue<T>(key, out T t))
            {
                t = await func();

                // Save data in cache.
                _cache.Set(key, t, cacheEntryOptions);
            }
            return t;
        }
    }
}
