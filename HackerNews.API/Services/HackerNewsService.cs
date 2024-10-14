using HackerNews.API.Extensions;
using HackerNews.API.Models;
using Microsoft.Extensions.Options;

namespace HackerNews.API.Services;

public class HackerNewsService(IOptions<HackerNewsServiceOptions> Options, HttpClient httpClient, ICacheService cacheService) : IHackerNewsService
{
    private static string GetStoryUrl(int id) => $"/v0/item/{id}.json";
    private static string GetIdsUrl => "/v0/beststories.json";

    public async Task<IEnumerable<StoryDto>> GetBestStoriesAsync(int count, CancellationToken cancellationToken = default)
    {
        return await cacheService.GetOrCreateAsync(
            $"BestStories_{count}",
            async () => await RequestDataAsync(count),
            TimeSpan.FromSeconds(Options.Value.StoriesExpirationInSeconds)
        );
    }

    private async Task<IEnumerable<StoryDto>> RequestDataAsync(int count, CancellationToken cancellationToken = default)
    {
        var bestStoryIds = await GetStoryIdsAsync(cancellationToken);

        var storyTasks = bestStoryIds.Take(count).Select(async id =>
        {
            var response = await httpClient.GetAsync($"{Options.Value.BaseUrl}{GetStoryUrl(id)}");
            await response.EnsureSuccessStatusCodeOrThrow(nameof(HackerNewsService));

            var storyDetails = await response.Content.ReadFromJsonAsync<HackerNewsStoryDto>();
            return new StoryDto(
                storyDetails!.Title,
                storyDetails.Url,
                storyDetails.By,
                DateTimeOffset.FromUnixTimeSeconds(storyDetails.Time).DateTime,
                storyDetails.Score,
                storyDetails.Descendants);
        });

        var stories = await Task.WhenAll(storyTasks);
        return stories.Where(p => p != null).OrderByDescending(p => p.Score).Take(count).ToList();
    }

    private async Task<IEnumerable<int>> GetStoryIdsAsync(CancellationToken cancellationToken = default)
    {
        return await cacheService.GetOrCreateAsync(
           $"BestStories_ids",
           async () =>
           {
               var response = await httpClient.GetAsync($"{Options.Value.BaseUrl}{GetIdsUrl}", cancellationToken);
               await response.EnsureSuccessStatusCodeOrThrow(nameof(HackerNewsService));
               return await response.Content.ReadFromJsonAsync<IEnumerable<int>>(cancellationToken) ?? [];
           },
           TimeSpan.FromSeconds(Options.Value.IdsExpirationInSeconds));
    }
}
