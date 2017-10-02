using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TestDotNetCore.Handlers;
using TestDotNetCore.Services;
using TestDotNetCore.Utils.FileLogger;
using System.IO;
using System.Collections.Generic;

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
			app.UseIconSpike();
			app.UseErrorHandler();

			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			loggerFactory.AddConsole(LogLevel.Information);
			loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "log.txt"));
			var logger = loggerFactory.CreateLogger("FileLogger");

			app.UseTimer();

			List<string> reqParams = new List<string>();
			foreach (string reqParam in this.requiredParams)
				reqParams.Add(reqParam);
			app.UseRequiredParams(reqParams);

			app.UseAuthToken();

			app.Run(async (context) =>
            {
				logger.LogInformation("Processing request {0}", context.Request.Path);
				var queryData = context.Request.Query;
				foreach (string key in queryData.Keys)
				{
					logger.LogInformation("{0}: {1}", key, queryData[key]);
				}
				int z = 1;
				foreach (string paramName in this.requiredParams)
					z *= int.Parse(context.Request.Query[paramName]);
                await context.Response.WriteAsync($"a * b * c = {z}");
			});
        }
    }
}
