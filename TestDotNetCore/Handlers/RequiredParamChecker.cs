using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;

namespace TestDotNetCore.Handlers
{
	public static class RequiredParamExtension
	{
		public static IApplicationBuilder UseRequiredParams(this IApplicationBuilder builder, List<string> requiredParams)
		{
			return builder.UseMiddleware<RequiredParamChecker>(requiredParams);
		}
	}

	public class RequiredParamChecker
    {
		private readonly RequestDelegate _next;
		private List<string> _reqParams;

		public RequiredParamChecker(RequestDelegate next, List<string> requiredParams)
		{
			this._next = next;
			this._reqParams = requiredParams;
		}

		public async Task Invoke(HttpContext context)
		{
			foreach (string param in this._reqParams)
			{
				if (context.Request.Query.ContainsKey(param))
				{
					var reqParam = context.Request.Query[param];
					if (string.IsNullOrWhiteSpace((string)reqParam) || !int.TryParse(reqParam, out int result))
						await context.Response.WriteAsync($"Parameter {param} is incorrect");
				}
				else
					await context.Response.WriteAsync($"Parameter {param} is missing");
			}
			await this._next.Invoke(context);
		}
    }
}
