namespace HackerNews.API.Models;

public record HackerNewsStoryDto(
    string By,
    int Descendants,
    int Score,
    long Time,
    string Title,
    string Type,
    string Url
);
