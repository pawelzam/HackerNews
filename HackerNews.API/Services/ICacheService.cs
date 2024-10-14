namespace HackerNews.API.Services;

public interface ICacheService
{
    Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> fetchFunction, TimeSpan expiration);
    void Remove(string cacheKey);
}
