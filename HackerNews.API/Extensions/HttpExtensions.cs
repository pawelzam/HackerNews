using HackerNews.API.Exceptions;

namespace HackerNews.API.Extensions;

public static class HttpExtensions
{
    public static async Task EnsureSuccessStatusCodeOrThrow(this HttpResponseMessage responseMessage, string apiName)
    {
        if (!responseMessage.IsSuccessStatusCode) 
        {
            var failedResponse = await responseMessage.Content.ReadAsStringAsync();
            throw new ApiInvokeException(apiName, failedResponse, responseMessage.StatusCode);
        }
    }
}
