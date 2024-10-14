using Microsoft.Extensions.Caching.Memory;

namespace HackerNews.API.Services;

public class MemoryCacheService(IMemoryCache memoryCache) : ICacheService
{
    private readonly IMemoryCache _memoryCache = memoryCache;

    public async Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> fetchFunction, TimeSpan expiration)
    {
        if (!_memoryCache.TryGetValue(cacheKey, out T? cacheEntry))
        {
            cacheEntry = await fetchFunction();
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };

            _memoryCache.Set(cacheKey, cacheEntry, cacheOptions);
        }

        return cacheEntry!;
    }

    public void Remove(string cacheKey)
    {
        _memoryCache.Remove(cacheKey);
    }
}

