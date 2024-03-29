﻿using Microsoft.Extensions.Caching.Memory;
using System;

namespace BlazorApp.Cache
{
    public class CacheMemoryHelper : ICacheBase
    {
        private IMemoryCache _cache;

        public CacheMemoryHelper(IMemoryCache cache)
        {
            _cache = cache;
        }


        public void Set<T>(T o, string? key)
        {
            T cacheEntry;

            // Look for cache key.
            if (!_cache.TryGetValue(key, out cacheEntry))
            {
                // Key not in cache, so get data.
                cacheEntry = o;

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromSeconds(7200)); // 2h

                // Save data in cache.
                _cache.Set(key, cacheEntry, cacheEntryOptions);
            }
        }


        public T Get<T>(string? key)
        {
            return _cache.Get<T>(key);
        }


        public void Remove(string? key)
        {
            _cache.Remove(key);
        }
    }
}
