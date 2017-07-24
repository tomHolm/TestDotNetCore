using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using TestDotNetCore.Services;

namespace TestDotNetCore.Handlers
{
	public static class TimerHandlerExtension
	{
		public static IApplicationBuilder UseTimer(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<TimerHandler>();
		}
	}

    public class TimerHandler
    {
		private readonly RequestDelegate _next;
		TimeService _timeServ;

		public TimerHandler(RequestDelegate next, TimeService timeService)
		{
			this._next = next;
			this._timeServ = timeService;
		}

		public async Task Invoke(HttpContext context)
		{
			if (context.Request.Path == "/time")
				await context.Response.WriteAsync($"current time: {this._timeServ.getTime()}");
			else
				await this._next.Invoke(context);
		}
    }
}
