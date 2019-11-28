using Microsoft.AspNetCore.Http;

namespace SecurityDemo.Services.Extensions
{
    public static class RequestExtensions
    {
        public static string UserIpAddress(this HttpRequest httpRequest)
        {
            return httpRequest.HttpContext.Connection.RemoteIpAddress?.ToString();
        }
    }
}
