# Hacker News API

This project is an ASP.NET Core Web API that retrieves and returns the first `n` "best stories" from the Hacker News API. The `n` is specified by the caller, and the stories are returned in descending order of their score.

## Features

- Fetches the details of the first `n` best stories from the Hacker News API.
- Stories are returned sorted by their score in descending order.
- The response contains the following details for each story:
  - **Title**
  - **URI (link to the story)**
  - **Posted By** (author)
  - **Time** (when the story was posted)
  - **Score**
  - **Comment Count**
- Uses caching to prevent overloading the Hacker News API.
- Flexible caching mechanism via an `ICacheService` interface, allowing different types of cache implementations.

## Limitations
- It follows all the limitation of source data API

## How to Run

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later installed.

### Setup

1. Clone this repository:

   ```bash
   git clone <repository_url>
   cd HackerNews
2. Restore dependencies:
   ```bash
   dotnet restore
3. Build the project:
   ```bash
   dotnet build
4. Run the project
   ```bash
   dotnet run --project HackerNews.API\HackerNews.API.csproj

### API Usage

Swagger: http://localhost:5035/swagger/index.html

Once the application is running, you can fetch the best stories by sending a `GET` request to:

GET /api/stories?count={n}

Where `{n}` is the number of top stories you want to retrieve. For example, to fetch the top 5 stories:

Example request:

GET http://localhost:5035/api/stories?count=10


#### Example Response

```json
[
    {
        "title": "A uBlock Origin update was rejected from the Chrome Web Store",
        "uri": "https://github.com/uBlockOrigin/uBlock-issues/issues/745",
        "postedBy": "ismaildonmez",
        "time": "2019-10-12T13:43:01+00:00",
        "score": 1716,
        "commentCount": 572
    },
    {
        "title": "...",
        "uri": "...",
        "postedBy": "...",
        "time": "...",
        "score": ...,
        "commentCount": ...
    }
]






