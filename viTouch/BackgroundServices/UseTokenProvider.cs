using Microsoft.AspNetCore.Builder;
using Service.Security;

namespace viTouch.BackgroundServices
{
	public static class UseTokenProvider
	{
		public static IApplicationBuilder UseToken(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<TokenMiddlewareProvider>();
		}
	}
}
