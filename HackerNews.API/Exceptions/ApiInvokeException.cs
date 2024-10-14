using System.Net;

namespace HackerNews.API.Exceptions;

public class ApiInvokeException(string apiName, string failedResponseContent, HttpStatusCode httpStatusCode) : Exception
{
    public string ApiName { get; } = apiName;
    public string FailedResponseContent { get; } = failedResponseContent;
    public HttpStatusCode HttpStatusCode { get; } = httpStatusCode;
}
