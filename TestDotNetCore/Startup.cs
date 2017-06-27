using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TestDotNetCore.Handlers;

namespace TestDotNetCore
{
    public class Startup
    {
		private string[] requiredParams = new string[] { "a", "b", "c" };

		public void ConfigureServices(IServiceCollection services)
        {
        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole();

			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			foreach (string reqParam in this.requiredParams)
				app.UseRequiredParams(reqParam);

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
