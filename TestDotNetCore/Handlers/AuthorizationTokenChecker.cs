using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace TestDotNetCore.Handlers
{
	public static class AuthorizationTokenExtension
	{
		public static IApplicationBuilder UseAuthToken(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<AuthorizationTokenChecker>();
		}
	}

	public class AuthorizationTokenChecker
    {
		private readonly RequestDelegate _next;

		public AuthorizationTokenChecker(RequestDelegate next)
		{
			this._next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			if (string.IsNullOrWhiteSpace(context.Request.Query["token"]))
				context.Response.StatusCode = 403;
			else
				await _next.Invoke(context);
		}
    }
}
