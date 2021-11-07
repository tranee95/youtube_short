using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Service.Security
{
    public class TokenMiddlewareProvider
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TokenMiddlewareProvider> _logger;
        private readonly string _token;

        public TokenMiddlewareProvider(RequestDelegate next, IConfiguration configuration, ILogger<TokenMiddlewareProvider> logger)
        {
            _next = next;
            _logger = logger;
            _token = configuration[AppSettings.AppToken];
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var requestToken = httpContext.Request.Headers[AppSettings.AppToken].ToString();
            if (requestToken == _token)
            {
                await _next.Invoke(httpContext);
                return;
            }
            
            _logger.LogDebug("{0} != {1}", _token, requestToken);
            httpContext.Response.StatusCode = 403;
            await httpContext.Response.WriteAsync("access denied");
        }
    }
}