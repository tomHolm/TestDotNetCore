using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace TestDotNetCore.Handlers
{
	public static class ErrorHandlerExtension
	{
		public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ErrorHandler>();
		}
	}

	public class ErrorHandler
	{
		private readonly RequestDelegate _next;

		public ErrorHandler(RequestDelegate next)
		{
			this._next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			await _next.Invoke(context);
			switch (context.Response.StatusCode)
			{
				case 403:
					await context.Response.WriteAsync("Access Denied");
					break;
				case 404:
					await context.Response.WriteAsync("Page not found");
					break;
			}
		}
	}
}
