namespace HackerNews.API.Models;

public record StoryDto(string Title, string Uri, string PostedBy, DateTime Time, int Score, int CommentCount);