namespace HackerNews.API.Services;

public class HackerNewsServiceOptions
{
    public string BaseUrl { get; set; } = string.Empty;
    public int IdsExpirationInSeconds { get; set; }
    public int StoriesExpirationInSeconds { get; set; }
}
