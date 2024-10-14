using HackerNews.API.Models;

namespace HackerNews.API.Services;
public interface IHackerNewsService
{
    Task<IEnumerable<StoryDto>> GetBestStoriesAsync(int count, CancellationToken cancellationToken);
}