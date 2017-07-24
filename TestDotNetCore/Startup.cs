using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TestDotNetCore.Handlers;
using TestDotNetCore.Services;

namespace TestDotNetCore
{
    public class Startup
    {
		private string[] requiredParams = new string[] {"a", "b", "c"};

		public void ConfigureServices(IServiceCollection services)
        {
			services.AddTimeService();
        }

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole();

			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			TimeService timeServ = app.ApplicationServices.GetService<TimeService>();

			app.UseErrorHandler();
			foreach (string reqParam in this.requiredParams)
				app.UseRequiredParams(reqParam);
			app.UseAuthToken();
			app.UseTimer();

            app.Run(async (context) =>
            {
				int z = 1;
				foreach (string paramName in this.requiredParams)
					z *= int.Parse(context.Request.Query[paramName]);
                await context.Response.WriteAsync($"a * b * c = {z}");
			});
        }
    }
}
