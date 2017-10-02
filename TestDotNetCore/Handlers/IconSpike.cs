using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace TestDotNetCore.Handlers
{
	public static class IconSpikeExtrension
	{
		public static IApplicationBuilder UseIconSpike(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<IconSpike>();
		}
	}

    public class IconSpike
    {
		private readonly RequestDelegate _next;
		private const string icoPath = "/favicon.ico";

		public IconSpike(RequestDelegate next)
		{
			this._next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			if (context.Request.Path != icoPath)
			{
				await this._next.Invoke(context);
			}
		}
    }
}
