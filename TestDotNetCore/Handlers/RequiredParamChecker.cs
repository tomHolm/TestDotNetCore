using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TestDotNetCore.Handlers
{
    public class RequiredParamChecker
    {
		private readonly RequestDelegate _next;
		private string _reqParam;

		public RequiredParamChecker(RequestDelegate next, string requiredParam)
		{
			this._next = next;
			this._reqParam = requiredParam;
		}

		public async Task Invoke(HttpContext context)
		{
			if (context.Request.Query.ContainsKey(this._reqParam))
			{
				var reqParam = context.Request.Query[this._reqParam];
				if (string.IsNullOrWhiteSpace((string)reqParam) || !int.TryParse(reqParam, out int result))
					await context.Response.WriteAsync($"Parameter {this._reqParam} is incorrect");
				else
					await this._next.Invoke(context);
			}
			else
				await context.Response.WriteAsync($"Parameter {this._reqParam} is missing");
		}
    }
}
