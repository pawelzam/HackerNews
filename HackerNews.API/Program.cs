using HackerNews.API.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var configuraiton = builder.Configuration;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICacheService, MemoryCacheService>();
builder.Services.Configure<HackerNewsServiceOptions>(options =>
{
    options.BaseUrl = configuraiton["ExternalService:HackerNewsService:BaseUrl"]!;
    options.IdsExpirationInSeconds = Convert.ToInt32(configuraiton["ExternalService:HackerNewsService:IdsExpirationInSeconds"]);
    options.StoriesExpirationInSeconds = Convert.ToInt32(configuraiton["ExternalService:HackerNewsService:StoriesExpirationInSeconds"]);
});
builder.Services.AddScoped<IHackerNewsService, HackerNewsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("stories", async ([FromQuery] int count, IHackerNewsService service, CancellationToken cancellationToken) => await  service.GetBestStoriesAsync(count, cancellationToken))
    .WithName("Get HackerNews Best Stories")
    .WithOpenApi();

app.Run();
